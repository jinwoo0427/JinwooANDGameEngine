using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        _aiMovementData.direction = Vector3.zero; //멈춰서
        //_aiMovementData.pointOfInterest = _enemyBrain.target.position;
        //_aiActionData.attack = true;
        if (_aiActionData.attack == false)
        {
            _enemyBrain.Attack(); //공격키가 눌리게 만드는거
            _aiMovementData.pointOfInterest = _enemyBrain.target.transform.position;
        }
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

    }
}