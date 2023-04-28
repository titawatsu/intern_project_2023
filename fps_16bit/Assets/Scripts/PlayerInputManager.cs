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

        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction sprintAction;

        private void Awake()
        {
            HideCursor();
            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            sprintAction = _currentMap.FindAction("Sprint");

            _moveAction.performed += onMove;
            _lookAction.performed += onLook;
            sprintAction.performed += onSprint;
            
            _moveAction.canceled += onMove;
            _lookAction.canceled += onLook;
            sprintAction.canceled += onSprint;
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

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _currentMap.Enable();
        }

        private void OnDisable()
        {
            _currentMap.Disable();
        }

    }

}