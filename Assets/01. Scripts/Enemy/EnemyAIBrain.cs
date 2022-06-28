using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIBrain : MonoBehaviour, IAgentInput
{
    [field: SerializeField] public UnityEvent<Vector3> OnMovementKeyPress { get; set; }
    [field: SerializeField] public UnityEvent<Vector3> OnPointerPositionChanged {get; set;}
    [field: SerializeField] public UnityEvent OnFireButtonPress {get; set;}
    [field: SerializeField] public UnityEvent OnFireButtonRelease {get; set;}

    public AIState _currentState;
    public AIState _walkState;

    public Transform target;
    public Transform basePosition = null;

    private AIActionData _aiActionData;
    public AIActionData AIActionData => _aiActionData;
    protected virtual void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
        _walkState = _currentState;
    }
    public void SetAttackState(bool state)
    {
        _aiActionData.attack = state;
    }
    
    private void Start()
    {
        
        
    }
    private void OnEnable()
    {
        target = GameManager.Instance.PlayerTrm;
        ChangeState(_walkState);
    }
    public void Attack()
    {
        OnFireButtonPress?.Invoke();
    }

    public void Move(Vector3 moveDirection, Vector3 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }

    public void ChangeState(AIState state)
    {
        _currentState = state; //상태 변경
    }

    protected virtual void Update()
    {
        if(target == null)
        {
            OnMovementKeyPress?.Invoke(Vector3.zero); //타겟 없으면 천천히 타겟으로 가
        }
        else
        {
            _currentState.UpdateState();
        }
    }
}
