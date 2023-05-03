using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;
using Unity.VisualScripting;

namespace fps_16bit
{
    public class ItemInteraction : MonoBehaviour
    {
        public Item item;
        private PlayerInputManager inputManager; // Get Player Input
        [SerializeField] private InventoryManager inventoryManager;
        
        [SerializeField] private LayerMask pickableLayerMask;
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private GameObject pickUpUi;
        [SerializeField] [Min(1)] private float hitRange = 3f;

        private RaycastHit hitInfo;
        private void Update()
        {
            // Draw Player Pickup's range
            Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
            
            ToggleHighlight();
        }
        private void PickUp()
        {
            if (inputManager.Interact)
            {
                InventoryManager.Instance.AddItem(item);
                Destroy(gameObject);
            }
        }

        private void ToggleHighlight()
        {
            if (hitInfo.collider != null)
            {
                hitInfo.collider.GetComponent<ItemHighlight>()?.ToggleHighlight(false);
            }

            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hitInfo, hitRange,
                    pickableLayerMask))
            {
                hitInfo.collider.GetComponent<ItemHighlight>()?.ToggleHighlight(true);
                pickUpUi.SetActive(true);
                PickUp();
            }
        }
    }
}
