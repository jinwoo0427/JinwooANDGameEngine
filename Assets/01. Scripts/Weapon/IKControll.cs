using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControll : MonoBehaviour
{
    public static IKControll instance;

    private Animator anim;

    [Header("Right Hand Ik")]
    [Range(0, 1)] public float rightHandWeight;
    public Transform rightHandObj = null;
    public Transform rightHandHint = null;

    [Header("Left Hand Ik")]
    [Range(0, 1)] public float leftHandWeight;
    public Transform leftHandObj = null;
    public Transform leftHandHint = null;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void OnIKPlay()
    {
        if (anim)
        {
            #region ������ �� IK

            if (rightHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }
            if (rightHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightHandHint.position);
            }


            #endregion

            #region ���� �� IK

            if (leftHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }
            if (leftHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHandHint.position);
            }


            #endregion
        }
    }
    //private void OnAnimatorIK()
    //{
        
    //}
}
