using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class ElevatorStartAnimation : MonoBehaviour
    {
        [SerializeField] private Animator elevatorAnim;
        void Start()
        {
            elevatorAnim.SetBool("open", true);

        }
    }
}
