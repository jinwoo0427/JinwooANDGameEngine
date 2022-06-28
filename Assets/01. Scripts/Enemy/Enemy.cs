using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolableMono, IAgent, IHittable, IKnockback
{
    [SerializeField] private EnemyDataSO _enemytData;
    public EnemyDataSO EnemyData => _enemytData;

    protected bool _isDead = false;
    protected AgentMovement _agentMovement; //���� �˹�ó���Ϸ��� �̸� �����´�.
    protected EnemyAnimation _enemyAnimation;
    protected EnemyAttack _enemyAttack;
    //protected CapsuleCollider _collider;

    protected EnemyAIBrain _enemyBrain;

    protected HealthBar _enemyHealthBar;

    //�׾����� ó���� �Ͱ�
    //��Ƽ�� ���¸� ������ �ְ�

    #region �������̽� ������
    public int Health { get; private set; }

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnReset { get; set; }

    public bool IsEnemy => true;
    public Vector3 HitPoint { get; private set; }

    public virtual void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;
        //���׾����� ����ٰ� �ǰ� ���� ���� �ۼ�
        float critical = Random.value; // 0 ~ 1 
        bool isCritical = false;

        if (critical <= GameManager.Instance.CriticalChance)
        {

            float ratio = Random.Range(GameManager.Instance.CriticalMinDamage,
                GameManager.Instance.CriticalMaxDamage);
            damage = Mathf.CeilToInt((float)damage * ratio);
            isCritical = true;
        }

        Health -= damage;
        HitPoint = damageDealer.transform.position; //���� ���ȴ°�? 
        //�̰� �˾ƾ� normal�� ����ؼ� �ǰ� Ƣ���� �� �� �ִ�.
        OnGetHit?.Invoke(); //�ǰ� �ǵ�� ���

        if(!_enemyBrain.AIActionData.attack)
            _enemyAnimation.PlayGetHitAnimation();

        //���⿡ ������ ���� ����ִ� ������ ���� �Ѵ�.
        DamagePopup popup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        popup.Setup(damage, transform.position + new Vector3(0, 2f, 0), isCritical, Color.white);

        if (Health <= 0)
        {
            _enemyHealthBar.gameObject.SetActive(false);

            DeadProcess();
        }
        else
        {
            _enemyHealthBar.SetHealth(Health);
        }
    }
    #endregion

    [SerializeField]
    protected bool _isActive = false;


    private void Awake()
    {
        _agentMovement = GetComponent<AgentMovement>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
        _enemyAttack = GetComponent<EnemyAttack>();
        //_collider = GetComponent<CapsuleCollider2D>();
        _enemyAttack.attackDelay = _enemytData.attackDelay;
        _enemyBrain = GetComponent<EnemyAIBrain>();
        _enemyHealthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
    }
    public void DeadProcess()
    {
        if (_isDead) return; //�̹� ����ó���� �ǰ� �ִ� �ֵ��� �ȹ޵���
        Health = 0;
        _enemyHealthBar.SetHealth(Health);
        _enemyHealthBar.enabled = false;

        _isDead = true;
        
        _agentMovement.StopImmediatelly(); //��� ����
        _agentMovement.enabled = false; //�̵��ߴ�
        //_enemyHealthBar._maxHealth = -1;
        OnDie?.Invoke(); //��� �̺�Ʈ �κ�ũ
        //�׾��� �� �ǵ���� �����Ѵ�.
    }
    public virtual void PerformAttack()
    {
        if (_isDead == false && _isActive == true)
        {
            //Debug.Log("�õ�");
            //���⿡ �������� ������ ������ �Ŵ�.
            _enemyAttack.Attack(_enemytData.damage);
        }
    }
    
    public override void ResetObject()
    {
        OnReset?.Invoke();
        _enemyHealthBar.enabled = true;
        _enemyHealthBar.gameObject.SetActive(true);

        _enemyHealthBar._maxHealth = -1;
            

        Health = _enemytData.maxHealth;
        _enemyHealthBar.SetHealth(Health);
        _isActive = true;
        _isDead = false;
        _agentMovement.enabled = true;


        _enemyAttack.Reset(); //ó�� �����ÿ� ��Ÿ�� �ٽ� ���ư��� 
        //��Ƽ�� ���� �ʱ�ȭ
        //Reset�� ���� �̺�Ʈ ����
        _agentMovement.ResetKnockbackParam();
    }
    
    private void Start()
    {
        //Health = _enemytData.maxHealth;
        //_enemyHealthBar.SetHealth(Health);
    }
    public void StopRotation()
    {
        _enemyBrain.target = null;
    }
    public void Die()
    {
        //Debug.Log(this.name + "��");
        //Ǯ�Ŵ����� �־��ְ�\
        
        PoolManager.Instance.Push(this);
        
        _isActive = false;

    }

    public void SpawnInPortal(Vector3 pos, float power, float time)
    {
        _isActive = false;
        transform.DOJump(pos, power, 1, time).OnComplete(() => _isActive = true);
    }

    public void Knockback(Vector3 direction, float power, float duration)
    {
        if (_isDead == false && _isActive == true)
        {
            
            if (power > _enemytData.knockbackRegist)
            {
                _agentMovement.KnockBack(direction, power, duration);
            }
        }
    }
}
