using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] private BulletDataSO _bulletData;
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _muzzle;


    public override void Attack(int damage)
    {
        
        if(_waitBeforeNextAttack == false)
        {
            _enemyBrain.SetAttackState(true); //���ݽ������� ����
            AttackFeedback?.Invoke(); 

            Transform target = GetTarget();
            
            Vector3 aimDirection = target.position - _firePos.position;
            float desireAngle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.AngleAxis(desireAngle, Vector3.up);

            SpawnBullet(_firePos.position, rot, true, damage);
            StartCoroutine(SpawnMuzzle());
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
    IEnumerator SpawnMuzzle()
    {
        _muzzle.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _muzzle.SetActive(false);
    }
    private void SpawnBullet(Vector3 pos, Quaternion rot, bool isEnemyBullet, int damage)
    {
        Bullet b = PoolManager.Instance.Pop(_bulletData.prefab.name) as Bullet;
        
        b.SetPositionAndRotation(pos, rot);
        b.IsEnemy = isEnemyBullet;
        b.BulletData = _bulletData;
        b.damageFactor = damage;
    }
}
