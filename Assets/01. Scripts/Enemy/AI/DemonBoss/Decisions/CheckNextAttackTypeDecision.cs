using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������� ������ ���ݰ� Ÿ���� ��ġ�ϴ��� üũ
public class CheckNextAttackTypeDecision : DemonBossDecison
{
    [SerializeField]
    private DemonBossAIBrain.AttackType attackType;

    public override bool MakeADecision()
    {
        return _phaseData.nextAttackType == attackType;
    }
}
