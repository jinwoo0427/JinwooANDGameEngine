using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyJumpAttack : EnemyAttack
{
    #region ������Ŀ�� ���� ����
    [SerializeField]
    private int _bazierResolution = 30; //������ Ŀ�� ���� ���� ���� ǥ��
    private Vector3[] _bazierPoints; //������ ����� ����

    #endregion

    #region ���� ���� ������
    [SerializeField]
    private float _jumpSpeed = 0.9f, _jumpDelay = 0.4f, _impactRadius = 2f;
    //���� �Ϸ������ �ɸ��� �ð�, ������ �����ϱ��������� ������ �ð�, ������ ó�´� ������
    private float _frameSpeed = 0; //�������� �����Ӵ� �ɸ��� �ð�, ����ؼ� �־��ٰ���
    #endregion

    public UnityEvent PlayJumpAnimation;   //���� ���� �ִϸ��̼� ��� ��ȣ
    public UnityEvent PlayLandingAnimation; //���� �ִϸ��̼� ��� ��ȣ

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(2);
        }
    }
    public override void Attack(int damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            _enemyBrain.SetAttackState(true);

            Jump();
        }
    }

    private void Jump()
    {
        _waitBeforeNextAttack = true;
        Vector3 deltaPos = transform.position - _enemyBrain.basePosition.position;
        Vector3 targetPos = GetTarget().position + deltaPos;
        Vector3 startControl = (targetPos - transform.position) / 4;

        float angle = targetPos.x - transform.position.x < 0 ? -45f : 45f;

        Vector3 cp1 = Quaternion.Euler(0, 0, angle) * startControl;
        Vector3 cp2 = Quaternion.Euler(0, 0, angle) * (startControl * 3);

        _bazierPoints = DOCurve.CubicBezier.GetSegmentPointCloud(
            transform.position, transform.position + cp1,
            targetPos, transform.position + cp2, _bazierResolution);
        _frameSpeed = _jumpSpeed / _bazierResolution;

        //����� �ڵ� ���߿� ���ﲨ��
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = _bazierPoints.Length;
        lr.SetPositions(_bazierPoints);

        //����
    }
}
