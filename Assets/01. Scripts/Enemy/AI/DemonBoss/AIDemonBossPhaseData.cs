using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIDemonBossPhaseData : MonoBehaviour
{

    /*
     * RocketPunch = 0,
        ShockPunch = 1,
        Fireball = 2,
        SummonPortal = 3,
    */

    public int phase;

    public bool isActive = false;

    public bool GeneratePunch;
    public bool ShockPunch;
    public bool JumpAttack;
    public bool Fireball;

    //public bool hasLeftArm;  //���� �� ������
    //public bool hasRightArm; //������ �� ������

    //���� �߿� �ϳ��� ��� �ִٸ�
    //public bool HasArms => hasLeftArm == true || hasRightArm == true;

    public DemonBossAIBrain.AttackType nextAttackType;

    public float idleTime;

    public bool CanAttack => GeneratePunch == false && ShockPunch == false && JumpAttack == false && Fireball == false;
}
