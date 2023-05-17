using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStartAnimation : MonoBehaviour
{
    [SerializeField] private Animator elevatorAnim;
    void Start()
    {
        elevatorAnim.SetBool("open", true);
            
    }
}
