using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentMovement : MonoBehaviour
{
    [SerializeField]
    protected MovementDataSO _movementSO;
    public MovementDataSO MovementSO
    {
        get => _movementSO;
    }

    protected float _currentVelocity = 3;
    protected Vector3 _movementDirection;

    public UnityEvent OnVelocityChange; //플레이어 속도가 바뀔때 실행될 이벤트
    public UnityEvent OnMove; //플레이어 속도가 바뀔때 실행될 이벤트


    #region 넉백관련 파라메터
    [SerializeField]
    protected bool _isKnockback = false;
    protected Coroutine _knockbackCo = null;

    #endregion

    protected bool _isRolling = false;
    public bool IsRolling
    {
        get => _isRolling;
    }

    protected Rigidbody _rigid;
    protected PlayerAnimation animator;

    public float speed = 6f;

    public float currentSpeed =>
        new Vector3(_rigid.velocity.x, _rigid.velocity.z).magnitude;


    public LayerMask camRayLayer;

    protected Vector3 moveInput;

    protected float fowardAmount;
    protected float turnAmount;

    protected Vector3 rollingDir;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        animator = GetComponent<PlayerAnimation>();

    }

    public void MoveAgent(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            if (Vector3.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalculateSpeed(movementInput);

        //var percent = _currentVelocity / _movementSO.maxSpeed;
        //animator.MouseDir(_movementDirection, percent);

    }

    private float CalculateSpeed(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += _movementSO.acceleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _movementSO.deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0, _movementSO.maxSpeed);
    }
   
    
    
    protected virtual void FixedUpdate()
    {
        if (_isKnockback == false)
        {
            _rigid.velocity = _movementDirection * _currentVelocity;
            
            //var percent = currentSpeed / _movementSO.maxSpeed;
            //animator.PlayMoveAnimation(moveInput, percent);
        }
        
        
    }


    //넉백구현할 때 사용할 거다.
    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
    }

    #region 넉백관련 구현부
    public void KnockBack(Vector3 direction, float power, float duration)
    {
        if (_isKnockback == false)
        {
            _isKnockback = true;
            direction.y = 0;
            _knockbackCo = StartCoroutine(KnockbackCoroutine(direction, power, duration));
        }

    }

    IEnumerator KnockbackCoroutine(Vector3 direction, float power, float duration)
    {
        _rigid.AddForce(direction.normalized * power, ForceMode.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockbackParam();
    }

    public void ResetKnockbackParam()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
        _isKnockback = false;
        _rigid.useGravity = false;
    }
    #endregion

    
}
