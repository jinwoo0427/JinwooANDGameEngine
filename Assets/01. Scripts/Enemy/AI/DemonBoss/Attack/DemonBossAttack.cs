using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DemonBossAttack : MonoBehaviour
{
    protected DemonBossAIBrain _aiBrain;
    public LayerMask whatIsTarget; // ���� ��� ���̾�


    public RaycastHit[] hits = new RaycastHit[10];
    protected virtual void Awake()
    {
        _aiBrain = transform.parent.GetComponent<DemonBossAIBrain>();
    }

    //������ ���������� ����Ǿ��ٸ� true��, ���� �����ߴٸ� false�� ������ ���������� �ٷ� �̾�����
    public abstract void Attack(Action<bool> Callback);


    public Transform GetTarget()
    {
        return _aiBrain.target;
    }
}
