using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAction : AIAction
{
    public override void TakeAction()
    {
        Vector3 dir = _enemyBrain.target.position - transform.position;
        dir.Normalize();
        _aiMovementData.direction = dir;
        //_aiMovementData.direction = _enemyBrain.target.position.normalized;
        //_aiMovementData.direction = Vector3.back;
        _aiMovementData.direction.y = 0;
        _aiMovementData.pointOfInterest = _enemyBrain.target.position;
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);   
    }
}
