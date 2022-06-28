using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        _aiMovementData.direction = Vector3.zero; //���缭
        //_aiMovementData.pointOfInterest = _enemyBrain.target.position;
        //_aiActionData.attack = true;
        if (_aiActionData.attack == false)
        {
            _enemyBrain.Attack(); //����Ű�� ������ ����°�
            _aiMovementData.pointOfInterest = _enemyBrain.target.transform.position;
        }
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

    }
}