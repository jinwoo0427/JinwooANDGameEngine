using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ƽ������� ������ �������� �̵��ϵ���
public class IsActiveDecision : DemonBossDecison
{
    public override bool MakeADecision()
    {
        return _phaseData.isActive;
    }
}
