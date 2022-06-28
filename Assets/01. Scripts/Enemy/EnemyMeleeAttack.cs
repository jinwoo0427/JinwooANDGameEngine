using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        if(_waitBeforeNextAttack == false)
        {
            //Debug.Log("시도2");
            _enemyBrain.SetAttackState(true); //공격시작으로 셋팅

            Vector3 dir = transform.position - _enemyBrain.transform.position;
            IHittable hittable = GetTarget().GetComponent<IHittable>();
            var direction = transform.forward;

            var size = Physics.SphereCastNonAlloc(transform.position, 1.2f, direction, hits, 1.7f,
                whatIsTarget);
            if (size != 0)
            {
                AttackFeedback?.Invoke();
                Debug.Log("공격성공");
                hittable?.GetHit(damage: damage, damageDealer: gameObject);
            }
            

            StartCoroutine(WaitBeforeAttackCoroutine());
            
        }
    }
}
