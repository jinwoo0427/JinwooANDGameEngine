using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class DemonBossAIBrain : EnemyAIBrain
{
    public enum AttackType
    {
        GeneratePunch = 0,
        ShockPunch = 1,
        Fireball = 2,
        JumpAttack = 3,
        //RightPunch = 4,
        //LeftPunch = 5,
    }

    public class EnemyAttackData
    {
        public DemonBossAttack atk;
        public UnityEvent animAction;
        public float time;
    }

    public Dictionary<AttackType, EnemyAttackData> _attackDic = new Dictionary<AttackType, EnemyAttackData>();
    

    protected AIDemonBossPhaseData _phaseData;
    public AIDemonBossPhaseData PhaseData => _phaseData;

    public UnityEvent OnFireBallCast = null;  //���̾ ĳ���� �̺�Ʈ
    public UnityEvent OnHandAttackCast = null; //�ָ԰��� �����ϱ��� �̺�Ʈ
    public UnityEvent OnKillAllEnemies = null;


    private Boss _boss;

    //private SummonPortalAttack _summonPortalAttack; //��Ż ������ ������ ���� �� �ʿ��ϴ� �����͵д�
    //private float _currentPortalCoolTime = 0;

    //���� ��Ʈ�� ���߿� SO��
    [SerializeField]
    private float _dealTimer = 10f, _generateTimerMax = 8f, _neutralTime = 10f;
        //_portalCoolTime = 10f;


    //�ǰݻ��°� ���ӵǴ� �ð�, 8�ʾȿ� ����ȭ, ����ȭ �ð�

    private float _generateTimer = 0f;
    private bool _isNeutral = false;
    private int _stunHP = 50;
    private int _bossHP = 500;
    private int _neutralCnt = 100; //100��ŭ�� ���� ��ġ

    private Queue<AttackType> _atkQueue;

    protected override void Awake()
    {
        base.Awake();
        _phaseData = transform.Find("AI").GetComponent<AIDemonBossPhaseData>();


        //�̺κ��� ���� SO�� ó���ؾ� ��.

        _boss = GetComponent<Boss>();
        _boss.HP = _bossHP; //���� ü�� 200���� ���� .

        _atkQueue = new Queue<AttackType>(); //����Ÿ�� ���� ť

        //_summonPortalAttack = transform.Find("AttackType").GetComponent<SummonPortalAttack>();
        SetDictionary();//���ݰ��� Dictionary ����
        
    }

    #region ������ �������� ���� ���� �ϴ� ��
    private void SetDictionary()
    {
        Transform attackTrm = transform.Find("AttackType");
        //�̰� ���߿� SO �� ���� �� �ִ�.
        EnemyAttackData fireballData = new EnemyAttackData
        {
            atk = attackTrm.GetComponent<FireBallAttack>(),
            animAction = OnFireBallCast,
            time = 1f
        };
        _attackDic.Add(AttackType.Fireball, fireballData);

        EnemyAttackData shockPunchData = new EnemyAttackData
        {
            atk = attackTrm.GetComponent<ShockPunchAttack>(),
            animAction = OnHandAttackCast,
            time = 2f
        };
        _attackDic.Add(AttackType.ShockPunch, shockPunchData);

        EnemyAttackData generatePunchData = new EnemyAttackData
        {
            atk = attackTrm.GetComponent<FlapperPunchAttack>(),
            animAction = OnHandAttackCast,
            time = 2f
        };
        _attackDic.Add(AttackType.GeneratePunch, generatePunchData);

        EnemyAttackData jumpData = new EnemyAttackData
        {
            atk = attackTrm.GetComponent<FlapperPunchAttack>(),
            animAction = OnHandAttackCast,
            time = 2f
        };
        _attackDic.Add(AttackType.JumpAttack, jumpData);

        //EnemyAttackData summonPortalData = new EnemyAttackData
        //{
        //    atk = attackTrm.GetComponent<SummonPortalAttack>(),
        //    animAction = OnFireBallCast,
        //    time = 5f
        //};
        //_attackDic.Add(AttackType.SummonPortal, summonPortalData);
    }
    #endregion

    protected override void Update()
    {
        //�׾��ٸ� �ƹ��͵� ���� �ʴ´�.
        if (_boss.IsDead == true) return;  

        if (_boss.State == Boss.BossState.Generate)
        {
            _generateTimer -= Time.deltaTime;
            if (_generateTimer <= 0)
            {
                _generateTimer = 0;
                SetInvincible(); //�������·� ��ȯ
            }
        }

        //����ȭ ���ٸ� ����ȭ �ð� ������ �ٽ� �������·� ��ȯ
        if(_boss.State == Boss.BossState.Neutral && _isNeutral == false)
        {
            _isNeutral = true;
            OnKillAllEnemies?.Invoke();
            StartCoroutine(DelayForNeutral());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //��� �� ���ó�� ġƮŰ
            OnKillAllEnemies?.Invoke();
        }

        if (_isNeutral) return;

        //��Ż ��Ÿ���� ���� �ִٸ� ��Ż ��
        //if (_currentPortalCoolTime > 0 )
        //{
        //    _currentPortalCoolTime -= Time.deltaTime;
        //}

        _currentState.UpdateState();
    }

    IEnumerator DelayForNeutral()
    {
        _phaseData.idleTime = _neutralTime + 0.5f; //���½ð���ŭ ���̵�

        yield return new WaitForSeconds(_neutralTime);
        SetInvincible();
        _isNeutral = false;
    }

    private void SetInvincible()
    {
        _boss.State = Boss.BossState.Invincible; //�������·� ��ȯ
        //_phaseData.nextAttackType = AttackType.SummonPortal;
        _phaseData.nextAttackType = AttackType.Fireball;
        _phaseData.idleTime = 1f; //1���� ��ȯ
    }

    public void Attack(AttackType type)
    {
        FieldInfo fInfo = typeof(AIDemonBossPhaseData).GetField(type.ToString(), BindingFlags.Public | BindingFlags.Instance);
        fInfo.SetValue(_phaseData, true);
        
        OnFireBallCast?.Invoke(); //ĳ���� ���� �ִϸ��̼� �ִٸ� ���

        EnemyAttackData atkData = null;
        _attackDic.TryGetValue(type,out atkData);

        if(atkData != null)
        {
            atkData.atk.Attack((result) => {
                _phaseData.idleTime = result == true ? atkData.time : 0.2f; //���� ���н� 0.2�� �̳��� �������� ����                
                SetNextAttackPattern();
                fInfo.SetValue(_phaseData, false);
            });

            atkData.animAction?.Invoke();
        }
    }

    private void SetNextAttackPattern()
    {
        //�������� ����
        //if(_summonPortalAttack.summonedPortalCnt <= 0 && _currentPortalCoolTime <= 0) //���� ������ ��Ż�� ���ٸ� �ٷ� ��Ż ����
        //{
        //    _phaseData.nextAttackType = AttackType.SummonPortal;
        //    _currentPortalCoolTime = _portalCoolTime;
        //    return;
        //}

        if(_atkQueue.Count == 0)
        {
            ShuffleAttackType();
        }
        
        _phaseData.nextAttackType = _atkQueue.Dequeue();        
    }

    private void ShuffleAttackType()
    {
        //���⸦ �ٲٸ� �ܼ� ������ �ƴ� ����ȭ�� ������ �� ���� �ִ�.
        Array types = Enum.GetValues(typeof(AttackType));

        List<AttackType> list = new List<AttackType>();
        foreach(AttackType t in types)
        {
            list.Add(t);
        }
        
        for(int i = 0; i < list.Count; i++)
        {
            int idx = Random.Range(0, list.Count - i);
            _atkQueue.Enqueue(list[idx]);
            list[idx] = list[list.Count - i - 1];            
        }
    }

    public void LostArm(bool isLeft)
    {
        //if(isLeft)
        //{
        //    _phaseData.hasLeftArm = false;
        //}
        //else
        //{
        //    _phaseData.hasRightArm = false;
        //}

        //��� ���� �Ҿ��ٸ� �ٿ���°� �Ǹ鼭 �ǰݰ�������.
        //if(_phaseData.HasArms == false)
        //{
        //    _boss.State = Boss.BossState.Damageable;
        //    StartCoroutine(DelayToGenerateArm());
        //}
        //if (_isNeutral == false)
        //{
        //    _boss.State = Boss.BossState.Damageable;
        //    StartCoroutine(DelayToGenerateArm());
        //}
    }

    public void HasDamageable(bool isDamageable)
    {
        if (isDamageable == true)
        {
            _boss.State = Boss.BossState.Damageable;
            StartCoroutine(DelayToGenerateArm());
        }
    }

    IEnumerator DelayToGenerateArm()
    {
        yield return new WaitForSeconds(_dealTimer);
        _generateTimer = _generateTimerMax; //8�� �ȿ� ����ȭ ����Ű�� �� ���
        //�ٷ� ��Ż ��ȯ

        //_phaseData.nextAttackType = AttackType.SummonPortal;
        _phaseData.nextAttackType = AttackType.Fireball;
        _phaseData.idleTime = 0f;
        _boss.NeutralCnt = _neutralCnt;
        _boss.State = Boss.BossState.Generate;
    }

    public void BossDead()
    {
        OnKillAllEnemies?.Invoke();
    }
}
