using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using fps_16bit;

namespace fps_16bit
{
    public class LockerManager : MonoBehaviour
    {
        public enum LockerState
        {
            Closed,
            Opened
        }

        public Text txtToDisplay;

        public LockerState State { get; internal set; }

        private bool playerInZone;                  //Check if the player is in the zone
        private bool isOpen;                    //Check if door is currently opened or not

        private Animation lockerAnim;
        private BoxCollider lockerCollider;           //To enable the player to go through the door if door is opened else block him

        public InputActionReference lockerAction;

        LockerState lockerState = new LockerState();

        private void OnEnable()
        {
            lockerAction.action.Enable();
            lockerAction.action.performed += LockerInteractHandle;
        }

        private void OnDisable()
        {
            lockerAction.action.Disable();
            lockerAction.action.performed -= LockerInteractHandle;
        }

        private void Start()
        {
            isOpen = false;                     //Is the door currently opened
            playerInZone = false;                   //Player not in zone
            lockerState = LockerState.Closed;           //Starting state is door closed

            txtToDisplay.enabled = false;

            lockerAnim = transform.parent.gameObject.GetComponent<Animation>();
            lockerCollider = transform.parent.gameObject.GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            playerInZone = true;
            txtToDisplay.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            playerInZone = false;
            txtToDisplay.enabled = false;
        }

        private void Update()
        {
            if (playerInZone)
            {
                if (lockerState == LockerState.Opened)
                {
                    txtToDisplay.GetComponent<Text>().text = "Press 'E' to Close";
                    lockerCollider.enabled = false;
                }
                else if (lockerState == LockerState.Closed)
                {
                    txtToDisplay.GetComponent<Text>().text = "Press 'E' to Open";
                    lockerCollider.enabled = true;
                }
            }
        }

        void LockerInteractHandle(InputAction.CallbackContext context)
        {
            if (playerInZone)
            {
                isOpen = !isOpen;           //The toggle function of door to open/close

                if (lockerState == LockerState.Closed && !lockerAnim.isPlaying)
                {
                    lockerAnim.Play("Locker_Open");
                    lockerState = LockerState.Opened;
                }

                if (lockerState == LockerState.Opened && !lockerAnim.isPlaying)
                {
                    lockerAnim.Play("Locker_Closed");
                    lockerState = LockerState.Closed;
                }

            }
        }
    }
}


