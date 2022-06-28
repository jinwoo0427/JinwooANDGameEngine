using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/Player")]
public class PlayerDataSO : ScriptableObject
{
    [Range(0, 0.9f)] public float critical; //ĳ������ ũ��Ƽ�� Ȯ��
    [Range(1.5f, 10f)] public float criticalMinDmg; //ũ��Ƽ�� �̴ϸ� ������
    [Range(1.5f, 20f)] public float criticalMaxDmg; //ũ��Ƽ�� �ƽ� ������

    [Range(0, 0.7f)] public float dodge; //ĳ���� ȸ��Ȯ��
    [Range(50, 100)] public int maxHP; //�ִ� ü��
    [Range(50, 100)] public int maxStamina; //�ִ� ���׹̳�

    [Range(1, 5)] public int maxWeapon = 3;

}
