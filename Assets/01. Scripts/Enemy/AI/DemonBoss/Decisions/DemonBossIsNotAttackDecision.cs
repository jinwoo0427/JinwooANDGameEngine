using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBossIsNotAttackDecision : DemonBossDecison
{
    public override bool MakeADecision()
    {
        //��� ������ �ϰ� ���� �ʴٸ� 
        return _phaseData.CanAttack;
    }
}
