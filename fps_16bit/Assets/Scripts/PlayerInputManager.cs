using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace fps_16bit
{
    public class PlayerInputManager : MonoBehaviour
    {

        [SerializeField] private PlayerInput PlayerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Sprint { get; private set; }
        public bool Jump { get; private set; }
        public bool Crouch { get; private set; }

        private InputActionMap currentMapAction;
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction sprintAction;
        private InputAction jumpAction;
        private InputAction crouchAction;

        private void Awake()
        {
            HideCursor();
            currentMapAction = PlayerInput.currentActionMap;
            moveAction = currentMapAction.FindAction("Move");
            lookAction = currentMapAction.FindAction("Look");
            sprintAction = currentMapAction.FindAction("Sprint");
            jumpAction = currentMapAction.FindAction("Jump");
            crouchAction = currentMapAction.FindAction("Crouch");

            moveAction.performed += onMove;
            lookAction.performed += onLook;
            sprintAction.performed += onSprint;
            jumpAction.performed += onJump;
            crouchAction.started += onCrouch;

            moveAction.canceled += onMove;
            lookAction.canceled += onLook;
            sprintAction.canceled += onSprint;
            jumpAction.canceled += onJump;
            crouchAction.canceled += onCrouch;
        }

        private void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }
        private void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }
        private void onSprint(InputAction.CallbackContext context)
        {
            Sprint = context.ReadValueAsButton();
        }
        private void onJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
        }
        private void onCrouch(InputAction.CallbackContext context)
        {
            Crouch = context.ReadValueAsButton();
        }
        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            currentMapAction.Enable();
        }

        private void OnDisable()
        {
            currentMapAction.Disable();
        }

    }

}