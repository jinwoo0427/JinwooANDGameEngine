using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using static Define;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)

    //            instance = FindObjectOfType<GameManager>();
    //        return instance;
    //    }
    //    set
    //    {
    //        instance = value;
    //    }
    //}


    [Header("Title Scene")]
    public string sceneName = "";

    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private PoolingListSO _initList = null;
    //[SerializeField] private TextureParticleManager _textureParticleManagerPrefab;
    public bool isGameover { get; private set; } // ���� ���� ����
    private Transform _playerTrm;

    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
            {
                //���߿� �÷��̾� ��ũ��Ʈ ����� Ÿ������ �����Ҳ�
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _playerTrm;
        }
    }
    public PlayerTest _player;
    public PlayerDataSO PlayerStatus
    {
        get
        {
            if (_player == null)
                _player = PlayerTrm.GetComponent<PlayerTest>();
            return _player.PlayerStatus;
        }
    }

    #region ���� ������ ���úκ�
    public UnityEvent<int> OnCoinUpdate = null;
    private int _coinCnt;
    public int Coin
    {
        get => _coinCnt;
        set
        {
            _coinCnt = value;
            OnCoinUpdate?.Invoke(_coinCnt);
        }
    }
    #endregion

    public Action OnClearAllDropItems = null;

    //#region �������� �ε� ���� �κе�
    //[Header("�������� �����͵�")]
    //public List<RoomListSO> stages;
    //private Room _currentRoom = null; //���� �ִ� ��
    //#endregion

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
            //Destroy(Instance);
        }
        Instance = this;

        PoolManager.Instance = new PoolManager(transform); //Ǯ�Ŵ��� ����

        //����� ���� �Ŵ��� ������ ��������
        GameObject timeController = new GameObject("TimeController");
        timeController.transform.parent = transform.parent;
        TimeController.instance = timeController.AddComponent<TimeController>();

        SetCursorIcon();
        CreatePool();
    }
    private void Start()
    {
        MoneyUI.instance.Addmoney(0);
    }
    public void TitleGo()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void AllClearItem()
    {
        OnClearAllDropItems?.Invoke();
    }
    public bool IsCritical => Random.value < PlayerStatus.critical;
    public int GetCriticalDamage(int damage)
    {
        float ratio = Random.Range(PlayerStatus.criticalMinDmg, PlayerStatus.criticalMaxDmg);
        damage = Mathf.CeilToInt((float)damage * ratio);
        return damage;
    }
    
    private void CreatePool()
    {
        foreach (PoolingPair pair in _initList.list)
            PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
    }

    private void SetCursorIcon()
    {
        Cursor.SetCursor(cursorTexture,
            new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f),
            CursorMode.Auto);
    }
    public void EndGame()
    {
        // ���� ���� ���¸� ������ ����
        isGameover = true;
        // ���� ���� UI�� Ȱ��ȭ
        GameUIManager.Instance.SetActiveGameoverUI(true);
    }
    public float CriticalChance { get => PlayerStatus.critical; }
    public float CriticalMinDamage { get => PlayerStatus.criticalMinDmg; }
    public float CriticalMaxDamage { get => PlayerStatus.criticalMaxDmg; }
}
