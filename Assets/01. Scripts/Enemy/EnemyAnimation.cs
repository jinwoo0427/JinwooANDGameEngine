using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{

    private Animator _agentAnimator;

    private EnemyAIBrain _enemyAIBrain;

    //Hash
    private readonly int _attackHashStr = Animator.StringToHash("Attack");
    private readonly int _hitHashStr = Animator.StringToHash("Hit");
    private readonly int _randomAtkHashStr = Animator.StringToHash("RandomAtk");
    protected readonly int _runHashStr = Animator.StringToHash("Run");
    protected readonly int _deathHashStr = Animator.StringToHash("Death");

    //���߿� ���ݾִϸ��̼��� �ִ� ������ ���� ����

    void Awake()
    {
        _agentAnimator = GetComponent<Animator>();
        _enemyAIBrain = GetComponent<EnemyAIBrain>();
    }

    public void SetEndOfAttackAnimation()
    {
        _enemyAIBrain.SetAttackState(false);
        //Debug.Log(" ������� ����");
    }
    public void PlayGetHitAnimation()
    {

        _agentAnimator.SetTrigger(_hitHashStr);
    }
    public void PlayAttackAnimation()
    {
        _agentAnimator.SetInteger(_randomAtkHashStr, Random.Range(1, 3));
        _agentAnimator.SetTrigger(_attackHashStr);
    }
    //public void PlayRunAnimation()
    //{
    //    _agentAnimator.SetTrigger();
    //}
    public void SetWalkAnimation(bool value)
    {
        _agentAnimator.SetBool(_runHashStr, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    public void PlayDeathAnimation()
    {
        _agentAnimator.SetInteger(_randomAtkHashStr, 0);
        _agentAnimator.SetTrigger(_deathHashStr);
    }
}
