using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 생성")]
    private List<Enemy> _spawnedEnemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    [SerializeField]
    private List<EnemyDataSO> _enemyList;
    [SerializeField]
    private int _count = 20;
    private int _spawnCount = 0, _deadCount = 0;
    [SerializeField]
    private float _minDelay = 0.8f, _maxDelay = 1.5f;

    public Transform parentObj;


    [Header("사용자 감지 관련")]
    [SerializeField]
    private float _detectRadius = 5f;
    [SerializeField]
    private LayerMask _playerMask;

    [SerializeField]
    private AudioClip _openClip, _closeClip;
    [SerializeField]
    private float _portalOpenDelay = 1f;

    private AudioSource _audioSource;
    private bool _isOpen = false;
    //private HealthBar _healthBar;

    private int wave = 1;
    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    [Header("포탈 상태 및 이벤트")]
    [SerializeField]
    private bool _sensorActive = false, _passiveActive = false;
    // passiveActive가 true이면 플레이어 접근여부와 상관없이 바로 활성화된다.
    public UnityEvent OnCloseWave = null; //웨이브가 끝날때 발생하는 이벤트
    public UnityEvent OnClearGame = null; 

    public List<SpawnWaveDataSO> waveData = new List<SpawnWaveDataSO>();
    

    private void Awake()
    {
        ResetSpanwer();
        //_animator = transform.Find("VisualSprite").GetComponent<Animator>();
        int playerLayer = LayerMask.NameToLayer("Player");
        _playerMask = 1 << playerLayer;
        _audioSource = GetComponent<AudioSource>();
        wave = 1;
        _passiveActive = true;
        //_healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        //WaveSpawn();
    }

    private void Start()
    {
        OpenPortal();
    }
    //포탈에게 플레이어를 감지하기 시작하라고 명령
    public void ActivatePortalSensor()
    {
        _sensorActive = true;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameover) return;


        //if (_isOpen == false)
        //{
        //    OpenPortal();
        //}

        //if (_isOpen == false && _sensorActive == true)
        //{
        //    if (_passiveActive == true) OpenPortal(); //나중에 만들꺼야
        //    else
        //    {
        //        var collider = Physics.OverlapSphere(transform.position, _detectRadius, _playerMask);
        //        if (collider != null) OpenPortal();
        //    }
        //}
    }
    public void WaveSpawn()
    {
        //print("스폰 시작");
        GameUIManager.Instance.UpdateWaveText(wave);
        switch (wave)
        {
            case 1:
                StartCoroutine(SpawnCoroutine(waveData[0]));
                break;
            case 2:
                StartCoroutine(SpawnCoroutine(waveData[1]));
                break;
            case 3:
                StartCoroutine(SpawnCoroutine(waveData[2]));
                break;
            case 4:
                StartCoroutine(SpawnCoroutine(waveData[3]));
                break;
            case 5:
                StartCoroutine(SpawnCoroutine(waveData[4]));
                break;
            case 6:
                StartCoroutine(SpawnCoroutine(waveData[5]));
                break;
            case 7:
                StartCoroutine(SpawnCoroutine(waveData[6]));
                break;
            case 8:
                StartCoroutine(SpawnCoroutine(waveData[7]));
                break;
        }
    }

    public void OpenPortal()
    {
        GameManager.Instance.OnClearAllDropItems?.Invoke();
        _isOpen = true;
        _audioSource.clip = _openClip;
        _audioSource.Play();

        //_healthBar.SetHealth(_count); //적의 갯수만큼 헬스바로 설정함

        WaveSpawn();
    }
    public void CloseWave()
    {
        if(wave == 8)
        {
            OnClearGame?.Invoke();
            return;
        }
        _spawnCount = 0;

        _deadCount = 0;
        _audioSource.clip = _closeClip;
        _audioSource.Play();
        wave++;
        _spawnedEnemies.Clear();
        OnCloseWave?.Invoke();



        //_spawnedEnemies.ForEach(x => x.DeadProcess()); //모든 적 사망 처리
        //_healthBar.gameObject.SetActive(false);
        //StartCoroutine(DestroyPortal());
    }
    IEnumerator SpawnCoroutine(SpawnWaveDataSO waveData)
    {
        _spawnCount = 0;
        yield return new WaitForSeconds(waveData.enemySpawnDelay); //포탈이 열리는 시간동안 대기
        //print("진짜 스폰시작");
        while(_spawnCount < waveData.enemySpawnCount) //소환된 갯수가 총 소환할 갯수보다 작은동안 반복
        {
            //여기를 나중에 가중치를 배워서 다시 만들어보세요. 가중치에 따라 나오도록
            int randomIndex = Random.Range(0, waveData._enemyList.Count);


            EnemyDataSO spawnEnemyData = waveData._enemyList[randomIndex];

            Transform posToSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            

            Enemy enemy = SpawnEnemy(posToSpawn.position, spawnEnemyData);

            _spawnedEnemies.Add(enemy);

            //enemy.SpawnInPortal(transform.position + (Vector3)randomOffset, power: 2f, time: 0.8f);

            UnityAction deadAction = null;
            deadAction = () =>
            {
                _deadCount++;
                //_healthBar.SetHealth(_count - _deadCount);
                if (_deadCount == waveData.enemySpawnCount)
                {
                    //print("샷따 내려");
                    CloseWave(); //샷따 내려!
                }
                enemy.OnDie.RemoveListener(deadAction);
            };
            enemy.OnDie.AddListener(deadAction);


            //enemy.OnDie.AddListener(enemies.Remove(enemy));
            // 사망한 적을 10 초 뒤에 파괴

            float waitTime = Random.Range(_minDelay, _maxDelay);
            _spawnCount++;

            yield return new WaitForSeconds(waitTime); //랜덤한 대기시간 이후 생성
        }
    
    }

    private Enemy SpawnEnemy(Vector3 posToSpawn, EnemyDataSO enemyData)
    {
        //print("스폰");
        
        posToSpawn.x = Random.Range(-5,5);
        posToSpawn.z = Mathf.Abs(posToSpawn.z);
        Enemy e = PoolManager.Instance.Pop(enemyData.enemyName) as Enemy;
        e.transform.position = posToSpawn;
        e.gameObject.transform.SetParent(parentObj);
        return e;
    }

    public void KillAllEnemyFromThisPortal()
    {
        _count = _spawnCount; //즉시 소환이 종료되도록 하고
        if (_spawnedEnemies.Count == 0)
        {
            CloseWave();
        }
        else
        {
            _spawnedEnemies.ForEach(x => x.DeadProcess()); //모든 적 사망 처리
        }
    }

    IEnumerator DestroyPortal()
    {
        yield return new WaitForSeconds(2f);
        
        OnCloseWave?.Invoke();

        gameObject.SetActive(false); //Destroy해버려도 된다.
    }
    public void SetPortalData(int cnt, bool passive)
    {
        this._count = cnt;
        this._passiveActive = passive;
        ActivatePortalSensor();
    }

    public void ResetSpanwer()
    {
        _isOpen = false;
        _deadCount = 0;
        _spawnCount = 0;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);
            Gizmos.color = Color.white;
        }
    }
#endif
}







