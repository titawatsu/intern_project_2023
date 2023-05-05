using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;
using UnityEngine.InputSystem;

namespace fps_16bit
{
    public class ItemInteraction : MonoBehaviour
    {
        
        [SerializeField] private LayerMask pickableLayerMask;
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private GameObject pickUpUi;
        [SerializeField] [Min(1)] private float hitRange = 3f;

        private RaycastHit hitInfo;

        public Camera playerCamera;
        public InputActionReference interactionAction;

        private void OnEnable()
        {
            interactionAction.action.Enable();
            interactionAction.action.performed += HandleInteraction;
        }

        private void OnDisable()
        {
            interactionAction.action.Disable();
            interactionAction.action.performed -= HandleInteraction;
        }

        private void Start()
        {
            pickUpUi.SetActive(false);
        }
        private void Update()
        {
            // Draw Player Pickup's range
            Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
            
            ToggleHighlight();
        }
        

        private void ToggleHighlight()
        {
            if (hitInfo.collider == null) pickUpUi.SetActive(false);
            if (hitInfo.collider != null)
            {
                hitInfo.collider.GetComponent<ItemHighlight>()?.ToggleHighlight(false);
                pickUpUi.SetActive(false);
            }
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hitInfo, hitRange,
                    pickableLayerMask))
            {
                hitInfo.collider.GetComponent<ItemHighlight>()?.ToggleHighlight(true);
                pickUpUi.SetActive(true);

            }
        }
        private void HandleInteraction(InputAction.CallbackContext context)
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hitInfo, hitRange, pickableLayerMask))
            {
                GameObject hitObject = hitInfo.collider.gameObject;
                PickupObject(hitObject);
                
            }
            
        }

        private void PickupObject(GameObject pickupObj)
        {
            Destroy(pickupObj);   
        }
       
    }
}
