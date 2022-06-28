using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    protected Animator _agentAnimator;
    //Hash
    protected readonly int _horizontalMoveHashStr = Animator.StringToHash("Horizontal Move");
    protected readonly int _verticalMoveHashStr = Animator.StringToHash("Vertical Move");
    protected readonly int _reloadHashStr = Animator.StringToHash("Reload");
    protected readonly int _shootHashStr = Animator.StringToHash("Shoot");
    protected readonly int _rollHashStr = Animator.StringToHash("Roll");
    protected readonly int _deathHashStr = Animator.StringToHash("Die");

    private void Awake()
    {
        _agentAnimator = GetComponent<Animator>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        // do nothing
    }
    public void PlayMoveAnimation(Vector2 moveDir, float percent)
    {
        _agentAnimator.SetFloat(_horizontalMoveHashStr, moveDir.x * percent, 0.01f, Time.deltaTime);
        _agentAnimator.SetFloat(_verticalMoveHashStr, moveDir.y * percent, 0.01f, Time.deltaTime);
    }
    public void PlayShootAnimation()
    {
        _agentAnimator.SetTrigger(_shootHashStr);
    }
    public void PlayReloadAnimation()
    {
        _agentAnimator.SetTrigger(_reloadHashStr);

    }
    public void PlayRollAnimation()
    {
        _agentAnimator.SetTrigger(_rollHashStr);
    }
    public void PlayDeathAnimation()
    {
        _agentAnimator.SetTrigger(_deathHashStr);
    }
    public void MouseDir(Vector3 moveInput, float percent)
    {
        _agentAnimator.SetFloat(_horizontalMoveHashStr, moveInput.x * percent, 0.05f, Time.deltaTime);
        _agentAnimator.SetFloat(_verticalMoveHashStr, moveInput.z * percent, 0.05f, Time.deltaTime);
    }
    
}