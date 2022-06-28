using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTest : MonoBehaviour, IAgent, IHittable, IKnockback
{
    [SerializeField] private PlayerDataSO _agentStatusSO;
    public PlayerDataSO PlayerStatus { get => _agentStatusSO; }

    [SerializeField]
    private int _health;
    public int Health
    {
        get => _health;
        set { _health = Mathf.Clamp(value, 0, _agentStatusSO.maxHP); }
    }
    
    private PlayerMovement _agentMovement;
    public PlayerMovement Playermovement
    {
        get => _agentMovement;
    }
    public GameObject hitpanel;
    public GameObject gameoverpanel;

    //사망처리를 위한 불리언 변수 하나 추가
    private bool _isDead = false;
    //플레이어 무기정보를 불러오기 위한 변수

    private PlayerWeapon _playerWeapon;
    public PlayerWeapon PWeapon { get => _playerWeapon; }

    private PlayerAnimation _playerAnimation;
    public PlayerAnimation PlayerAnim
    {
        get => _playerAnimation;
    }

    public PlayerHealthBar _playerHealthbar;
    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    public bool IsEnemy => false;

    public Vector3 HitPoint { get; private set; }

    public float critical;
    public int criticalMinDmg;
    public int criticalMaxDmg;
    public int maxHp;
    public int stamina;


    public void ChangeIk(Transform rightHand, Transform leftHand)
    {
        IKControll.instance.rightHandObj = rightHand;
        IKControll.instance.leftHandObj = leftHand;
    }
    public void ResetPlayerData()
    {
        _agentStatusSO.critical = critical;
        _agentStatusSO.criticalMinDmg = criticalMinDmg;
        _agentStatusSO.criticalMaxDmg = criticalMaxDmg;
        _agentStatusSO.maxHP = maxHp;
        _agentStatusSO.maxStamina = stamina;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;

        Health -= damage;
        _playerHealthbar.Damage(damage);
        
        StartCoroutine(HitPanel());
        OnGetHit?.Invoke();
        if (Health <= 0)
        {
            _playerHealthbar.Damage(damage);
            OnDie?.Invoke();
            ChangeIk(null, null);
            _isDead = true;
            StartCoroutine(GameOverUI());
        }
    }
    IEnumerator GameOverUI()
    {
        yield return new WaitForSeconds(4f);
        gameoverpanel.SetActive(true);
        
    }
    IEnumerator HitPanel()
    {
        hitpanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitpanel.SetActive(false);
    }
    public PlayerWeapon playerWeapon
    {
        get => _playerWeapon;
    }
    private float offset = 192f;
    private void Awake()
    {
        _playerWeapon = transform.Find("WeaponParent").GetComponent<PlayerWeapon>();
        _agentMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerHealthbar = GetComponent<PlayerHealthBar>();
        //_playerHealthbar = transform.Find("HealthPanel").GetComponent<PlayerHealthBar>();
        ResetPlayerData();
        //gameoverpanel.SetActive(false);
    }

    private void Start()
    {
        Health = _agentStatusSO.maxHP;
        _playerHealthbar.InitHp(Health);
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Front"))
    //    {
    //        MapSpawner.Instance.SpawnFront();
    //        print("앞에 생성");
    //    }
    //    if (other.CompareTag("Back"))
    //    {
    //        MapSpawner.Instance.SpawnBack();
    //        print("뒤에 생성");

    //    }
    //}
    private void OnAnimatorIK()
    {
        IKControll.instance.OnIKPlay();

    }
    public void Knockback(Vector3 direction, float power, float duration)
    {
        _agentMovement.KnockBack(direction, power, duration);
    }

    public bool ApplyDamage(DamageMessage damageMessage)
    {
        throw new System.NotImplementedException();
    }
}
