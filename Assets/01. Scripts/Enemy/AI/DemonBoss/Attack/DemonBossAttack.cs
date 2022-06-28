using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DemonBossAttack : MonoBehaviour
{
    protected DemonBossAIBrain _aiBrain;
    public LayerMask whatIsTarget; // 추적 대상 레이어


    public RaycastHit[] hits = new RaycastHit[10];
    protected virtual void Awake()
    {
        _aiBrain = transform.parent.GetComponent<DemonBossAIBrain>();
    }

    //공격이 성공적으로 수행되었다면 true를, 만약 실패했다면 false를 보내서 다음공격이 바로 이어지게
    public abstract void Attack(Action<bool> Callback);


    public Transform GetTarget()
    {
        return _aiBrain.target;
    }
}
