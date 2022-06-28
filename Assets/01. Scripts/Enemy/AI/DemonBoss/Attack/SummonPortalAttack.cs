using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SummonPortalAttack : DemonBossAttack
{
    private DemonBossAIBrain _brain;

    [SerializeField]
    private int _summonCnt = 2;
    private Vector3[] _summonPoints;

    public int summonedPortalCnt = 0;

    protected override void Awake()
    {
        base.Awake();
        _brain = transform.parent.GetComponent<DemonBossAIBrain>();
        Transform summonTrm = transform.Find("SummonArea");

        _summonPoints = new Vector3[summonTrm.childCount];
        
        for(int i = 0; i < _summonPoints.Length; i++)
        {
            _summonPoints[i] = summonTrm.GetChild(i).position;
        }

        _summonCnt = Mathf.Clamp(_summonCnt, 0, _summonPoints.Length);

        summonedPortalCnt = 0; //���� ��ȯ�� ��Ż ����
    }

    public override void Attack(Action<bool> Callback)
    {
        //StartCoroutine(SummonPortal(Callback));
    }

    //IEnumerator SummonPortal(Action<bool> Callback)
    //{
    //    //�̹� ��ȯ�� ��Ż�� �ִٸ� �����ϰ� �ٷ� ��������
    //    if(summonedPortalCnt > 0)
    //    {
    //        Callback.Invoke(false);
    //        yield break;
    //    }
    //    //������ ��ġ�� ������ŭ ��� �ش� ��ġ�� �ڷ�ƾ���� ��Ż�� ��ȯ
    //    Vector3[] selectPos = new Vector3[_summonCnt]; //��ȯ�� ������ŭ ���� ��ġ �ľ�

    //    int[] posArr = new int[_summonPoints.Length]; //posArr �� 0,1,2,3 �ִ´�.
    //    for(int i = 0; i < posArr.Length; i++)
    //    {
    //        posArr[i] = i;
    //    }

    //    for(int i = 0; i < selectPos.Length; i++)
    //    {
    //        int idx = Random.Range(0, posArr.Length - i);  // 0, 1, 2, 3
    //        selectPos[i] = _summonPoints[posArr[idx]];

    //        posArr[idx] = posArr[posArr.Length - i - 1]; 
    //    }

    //    //������� ���� selectPos�� ��ǥ ���� �Ϸ�
        
    //    for(int i = 0; i < selectPos.Length; i++)
    //    {
    //        EnemySpawner es = PoolManager.Instance.Pop("SmallPortal") as EnemySpawner;
    //        es.transform.position = selectPos[i];
    //        es.SetPortalData(cnt:5, passive: true); //5������ �нú�� ��ȯ

    //        summonedPortalCnt++;
    //        //��ȯ�� ��Ż�� �극�ο��� ������� �Ҷ� ��������.
    //        UnityAction action = () =>
    //        {
    //            es.KillAllEnemyFromThisPortal();
    //        };

    //        _aiBrain.OnKillAllEnemies.AddListener(action);

    //        //��Ż�� ������ �̺�Ʈ ���� ����
    //        UnityAction closeAction = null;
    //        closeAction = () =>
    //        {
    //            summonedPortalCnt--;
    //            _aiBrain.OnKillAllEnemies.RemoveListener(action);
    //            es.OnCloseWave.RemoveListener(closeAction);
    //        };
    //        es.OnCloseWave.AddListener(closeAction);

    //        yield return new WaitForSeconds(1f); //1�� ��������
    //    }

    //    Callback?.Invoke(true);
    //}
}
