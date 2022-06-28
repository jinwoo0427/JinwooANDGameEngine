using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        if(_waitBeforeNextAttack == false)
        {
            //Debug.Log("�õ�2");
            _enemyBrain.SetAttackState(true); //���ݽ������� ����

            Vector3 dir = transform.position - _enemyBrain.transform.position;
            IHittable hittable = GetTarget().GetComponent<IHittable>();
            var direction = transform.forward;

            var size = Physics.SphereCastNonAlloc(transform.position, 1.2f, direction, hits, 1.7f,
                whatIsTarget);
            if (size != 0)
            {
                AttackFeedback?.Invoke();
                Debug.Log("���ݼ���");
                hittable?.GetHit(damage: damage, damageDealer: gameObject);
            }
            

            StartCoroutine(WaitBeforeAttackCoroutine());
            
        }
    }
}
