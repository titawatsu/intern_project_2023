using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RighthandIK : MonoBehaviour
{
    public Transform rightHand;
    public Animator anim;

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
    }
}
