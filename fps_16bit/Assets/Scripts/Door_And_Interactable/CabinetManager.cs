using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CabinetManager : MonoBehaviour
{
    public Text txtCabinet;

    private bool playerInZone;
    private bool cabinetOpen;

    private Animator anim;
    public BoxCollider cabinetCollider;
    
    public InputActionReference cabinetAction;

    private enum CabinetState
    {
        Closed,
        Open
    }

    private CabinetState cabinetState = new CabinetState();

    private void OnEnable()
    {
        cabinetAction.action.Enable();
        cabinetAction.action.performed += CabinetInteractHandle;
    }

    private void OnDisable()
    {
        cabinetAction.action.Disable();
        cabinetAction.action.performed -= CabinetInteractHandle;
    }

    private void Start()
    {
        cabinetOpen = false;
        playerInZone = false;
        cabinetState = CabinetState.Closed;
    }

    private void OnTriggerEnter(Collider other)
    {
        txtCabinet.enabled = true;
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
        txtCabinet.enabled = false;
    }

    private void Update()
    {
        if (playerInZone)
        {
            if (cabinetState == CabinetState.Open)
            {
                txtCabinet.GetComponent<Text>().text = "Press 'E' to Close";
            }
            else if (cabinetState == CabinetState.Closed)
            {
                txtCabinet.GetComponent<Text>().text = "Press 'E' to Open";
            }
        }
    }

    void CabinetInteractHandle(InputAction.CallbackContext context)
    {
        if (playerInZone)
        {
            cabinetOpen = !cabinetOpen;

            anim.SetBool("isOpen", cabinetOpen);
            
        }
    }
}
