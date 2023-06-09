using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using fps_16bit;

namespace fps_16bit
{
    public class GeneratorTrigger : MonoBehaviour
    {
        public TMP_Text generatorText;

        public int gasAmountPlayer;

        public int gasAmountNeed;

        public bool playerInzone;

        public InputActionReference interactionAction;

        public Animator elevatorAnim;

        private void OnEnable()
        {
            interactionAction.action.Enable();
            interactionAction.action.performed += GenEventInteraction;
        }
        private void OnDisable()
        {
            interactionAction.action.Disable();
            interactionAction.action.performed -= GenEventInteraction;
        }
        private void Start()
        {
            gasAmountNeed = 4;
            generatorText.enabled = false;
            playerInzone = false;
        }

        private void Update()
        {

            gasAmountPlayer = Player.instance.gasAmount;

            generatorText.text = "Jerry Can " + gasAmountPlayer + "/" + gasAmountNeed;

            if (gasAmountPlayer >= gasAmountNeed)
            {
                generatorText.GetComponent<Text>().text = "Press 'E' Pour gasoline on generator";
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            playerInzone = true;
            generatorText.enabled = true;

        }

        private void OnTriggerExit(Collider other)
        {
            playerInzone = false;
            generatorText.enabled = false;
        }

        private void GenEventInteraction(InputAction.CallbackContext context)
        {

            if (gasAmountPlayer < gasAmountNeed) return;

            if (playerInzone)
            {
                elevatorAnim.SetBool("open", true);
                generatorText.gameObject.SetActive(false);
            }
        }

    }

}