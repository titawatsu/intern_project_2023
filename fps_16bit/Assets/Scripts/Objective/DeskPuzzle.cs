using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace fps_16bit
{
    
    public class DeskPuzzle : MonoBehaviour
    {
        public Text bookText;
        public TMP_Text storyText;
    
        public GameObject book;

        public int bookAmountPlayer;
        public int bookAmountNeed = 1;

        public Animator elevatorAnim;
        public InputActionReference interactionAction;

        private bool playerInzone = false;
        private void Start()
        {
            book.SetActive(false);
            bookText.enabled = false;
            storyText.enabled = false;
        }
        private void OnEnable()
        {
            interactionAction.action.Enable();
            interactionAction.action.performed += BookEventInteract;
        }
        private void OnDisable()
        {
            interactionAction.action.Disable();
            interactionAction.action.performed -= BookEventInteract;
        }

        private void Update()
        {
            bookAmountPlayer = Player.instance.bookAmount;
        }

        private void OnTriggerEnter(Collider other)
        {
            playerInzone = true;
            bookText.enabled = true;

            bookText.text = "Press 'E' to place the book";
            
            storyText.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            playerInzone = false;
            bookText.enabled = false;
            storyText.enabled = false;
        }

        private void BookEventInteract(InputAction.CallbackContext context)
        {

            if (bookAmountPlayer < bookAmountNeed) return;

            if (playerInzone)
            {
                book.SetActive(true);

                bookText.enabled = false;
                bookText.gameObject.SetActive(false);

                storyText.text = "When you place the book, something happens..";
                
                elevatorAnim.SetBool("open", true);
            }
        }
    }
}
