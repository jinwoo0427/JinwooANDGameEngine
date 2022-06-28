using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NeutralEnemy : Enemy
{
    protected bool _isNeutral = false;  //���� ���°� �Ǹ� true
    protected int _accumulateDamage = 0; //����������

    [SerializeField]
    protected int _neutralDamage = 10;
    [SerializeField]
    protected float _neutralTime = 3f; //����ȭ �ð�
    public UnityEvent NeutralHit = null; //���»����϶� ������ ���� ���
    public UnityEvent OnEnterNeutral = null;
    public UnityEvent OnExitNeutral = null;

    public override void GetHit(int damage, GameObject damageDealer)
    {
        //����߰ų� �������϶��� ������ �ȹ޵��� �Ѵ�.
        if (_isDead || _enemyBrain.AIActionData.attack) return;

        if(_isNeutral)
        {
            NeutralHit?.Invoke();
        }else
        {
            base.GetHit(damage, damageDealer);
            _accumulateDamage += damage; //�� 10�� ���������� ����ȭ�� ��������
        }
        
        if(_isNeutral == false && _accumulateDamage >= _neutralDamage)
        {
            _accumulateDamage = 0;
            _isNeutral = true;
            OnEnterNeutral?.Invoke();
            StartCoroutine(NeutralCoroutine());
        }
    }

    IEnumerator NeutralCoroutine()
    {
        yield return new WaitForSeconds(_neutralTime);
        _isNeutral = false;
        OnExitNeutral?.Invoke();
    }

    public override void ResetObject()
    {
        base.ResetObject();
        _accumulateDamage = 0;
        _isNeutral = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public override void PerformAttack()
    {
        //���»����϶��� ���� ���ϰ�
        if (_isNeutral)
            return;
        base.PerformAttack();
    }
}
