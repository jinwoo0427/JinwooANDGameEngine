using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//idleTime�� 0���� �۰ų� ���ƾ� true
public class DelayTimeDecision : DemonBossDecison
{
    public override bool MakeADecision()
    {
        return _phaseData.idleTime <= 0;  
    }
}
