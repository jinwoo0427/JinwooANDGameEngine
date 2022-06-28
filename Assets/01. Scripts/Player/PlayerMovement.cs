using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerMovement : AgentMovement
{
    public PlayerDataSO _playerSO;
    public PlayerStamina _playerStamina;
    public MotionTrail motion;
    [SerializeField]
    private int _stamina;
    public int Stamina
    {
        get => _stamina;
        set { _stamina = Mathf.Clamp(value, 0, _playerSO.maxHP); }
    }
    private void Start()
    {
        _playerStamina.InitStamina(_playerSO.maxStamina);
    }
    protected override void FixedUpdate()
    {
        if (_isKnockback == false && _isRolling == false)
        {
            _rigid.velocity = _movementDirection * _currentVelocity;

            Tuning();

            ConvertMoveInput();
            OnMove?.Invoke();
            _playerStamina.HealStamina(0.5f);
            var percent = _currentVelocity / _movementSO.maxSpeed;
            animator.MouseDir(moveInput, percent);
        }
        else
        {
            _rigid.velocity = rollingDir * 10f;
        }

        if (_currentVelocity <= 0f)
        {
            OnVelocityChange?.Invoke();
        }

    }

    public void Rolling()
    {
        if (_isRolling == false && _playerStamina.currentStamina >= 20f)
        {
            _isRolling = true;


            transform.forward = _movementDirection;
            rollingDir = _movementDirection;

            _playerStamina.MinusStamina(40f);
            //_rigid.AddForce(_movementDirection * 10f, ForceMode.Impulse);
            animator.PlayRollAnimation();
        }

    }
    public void RollingEnd()
    {
        _isRolling = false;
    }
    private void Tuning()
    {
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, camRayLayer))
        {
            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion rot = Quaternion.LookRotation(playerToMouse);

            _rigid.MoveRotation(rot);
        }

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
    }
    private void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(_movementDirection);

        turnAmount = localMove.x;
        fowardAmount = localMove.z;
        moveInput = new Vector3(turnAmount,0, fowardAmount);

    }

}
