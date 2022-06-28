using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBossBodyAnimation : MonoBehaviour
{
    protected Animator _bodyAnimator;

    protected readonly int _hashShockPunch = Animator.StringToHash("ShockPunch");
    protected readonly int _hashPunch = Animator.StringToHash("GeneratePunch");
    protected readonly int _hashFireBall = Animator.StringToHash("Fireball");
    protected readonly int _hashGenerateBool = Animator.StringToHash("Generate");
    protected readonly int _hashNeutralBool = Animator.StringToHash("Neutral");
    protected readonly int _hashJumpAttack = Animator.StringToHash("JumpAttack");
    protected readonly int _hashJumpDown = Animator.StringToHash("JumpDown");
    protected readonly int _hashWalk = Animator.StringToHash("Walk");
    protected readonly int _hashDead = Animator.StringToHash("Dead");

    //private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _bodyAnimator = GetComponent<Animator>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void PlayJumpStart()
    {
        _bodyAnimator.SetTrigger(_hashJumpAttack);
    }
    public void PlayJumpDown()
    {
        _bodyAnimator.SetTrigger(_hashJumpDown);
    }
    public void PlayDeadAnimation()
    {
        _bodyAnimator.SetTrigger(_hashDead);
    }

    public void PlayPunchAnimation()
    {
        _bodyAnimator.SetTrigger(_hashPunch);
    }
    public void PlayShockPunchAnimation()
    {
        _bodyAnimator.SetTrigger(_hashShockPunch);
    }

    public void PlayFireBallAnimation()
    {
        _bodyAnimator.SetTrigger(_hashFireBall);
    }
    public void PlayWalkArm(bool value)
    {
        _bodyAnimator.SetBool(_hashWalk, value);
    }
    public void EnterGenerate(bool value)
    {
        _bodyAnimator.SetBool(_hashGenerateBool, value);
    }

    public void EnterNeutral(bool value)
    {
        _bodyAnimator.SetBool(_hashNeutralBool, value);
    }

    public void SetInvincible(bool value)
    {
        //_spriteRenderer.material.SetInt("_MakeNeutralColor", value ? 1 : 0);
    }
}
