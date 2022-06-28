using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyJumpAttack : EnemyAttack
{
    #region 베지어커브 관련 값들
    [SerializeField]
    private int _bazierResolution = 30; //베지어 커브 상의 점의 갯수 표현
    private Vector3[] _bazierPoints; //베지어 곡선상의 점들

    #endregion

    #region 점프 관련 변수들
    [SerializeField]
    private float _jumpSpeed = 0.9f, _jumpDelay = 0.4f, _impactRadius = 2f;
    //점프 완료까지의 걸리는 시간, 점프를 시작하기전까지의 딜레이 시간, 점프로 처맞는 반지름
    private float _frameSpeed = 0; //점프에서 프레임당 걸리는 시간, 계산해서 넣어줄거임
    #endregion

    public UnityEvent PlayJumpAnimation;   //점프 시작 애니메이션 재생 신호
    public UnityEvent PlayLandingAnimation; //착지 애니메이션 재생 신호

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

        //디버그 코드 나중에 지울꺼임
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = _bazierPoints.Length;
        lr.SetPositions(_bazierPoints);

        //여기
    }
}
