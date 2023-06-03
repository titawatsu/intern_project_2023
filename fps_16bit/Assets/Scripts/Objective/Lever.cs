using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using fps_16bit;

namespace fps_16bit
{
    public class Lever : MonoBehaviour
    {
        [SerializeField] private GameObject controller;

        [SerializeField] private Animation anim;
        [SerializeField] private Collider leverCollider;

        [SerializeField] private Text leverText;

        public GameObject bulb;

        [HideInInspector] public bool turned = false;
        [HideInInspector] public bool canTurn = true;

        
        private bool playerInzone;

        public Material bulbOff;
        public Material bulbOn;

        public InputActionReference leverInput;

        private void Start()
        {
            leverText.enabled = false;
        }

        IEnumerator onTurned()
        {
            yield return new WaitForSeconds(0.875f);
            canTurn = true;
            turned = !turned;

            if (turned)
            {
                bulb.GetComponent<Renderer>().material = bulbOn;
            }
            else
            {
                bulb.GetComponent<Renderer>().material = bulbOff;
            }
            controller.GetComponent<LeverPuzzle>().RecieveSignal(gameObject, turned);
        }

        private void OnTriggerEnter(Collider other)
        {
            playerInzone = true;
            Debug.Log("In Zone");
            leverText.enabled = true;

        }

        private void OnTriggerExit(Collider other)
        {
            playerInzone = false;
            Debug.Log("Not In Zone");
            leverText.enabled = false;
        }

        private void OnEnable()
        {
            leverInput.action.Enable();
            leverInput.action.performed += LeverInteraction;
        }
        private void OnDisable()
        {
            leverInput.action.Disable();
            leverInput.action.performed -= LeverInteraction;
        }

        private void LeverInteraction(InputAction.CallbackContext context)
        {
            if (!playerInzone) return;

            if (canTurn)
            {
                canTurn = false;
                if (!turned)
                {
                    anim.Play("Triggered");
                    StartCoroutine(onTurned());
                }
                else
                {
                    anim.Play("PullUp");
                    StartCoroutine(onTurned());
                }
            }
            
            
        }
    }
}
