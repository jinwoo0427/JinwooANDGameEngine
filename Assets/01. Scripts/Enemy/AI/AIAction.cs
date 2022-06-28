using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;
    [SerializeField]
    protected EnemyAIBrain _enemyBrain;

    protected virtual void Awake()
    {
        //_aiActionData = transform.GetComponentInParent<AIActionData>();
        //_aiMovementData = transform.GetComponentInParent<AIMovementData>();
        //_enemyBrain = transform.GetComponentInParent<EnemyAIBrain>();

        _enemyBrain = transform.GetComponentInParent<EnemyAIBrain>();
        _aiActionData = _enemyBrain.transform.GetComponentInChildren<AIActionData>();
        _aiMovementData = _enemyBrain.transform.GetComponentInChildren<AIMovementData>();
    }

    

    public abstract void TakeAction();
}
