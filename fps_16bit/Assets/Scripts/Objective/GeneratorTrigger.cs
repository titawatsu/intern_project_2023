using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GeneratorTrigger : MonoBehaviour
{
    public Text generatorText;

    public int gasAmountPlayer;
    
    public int gasAmountNeed;

    public bool playerInzone;

    public InputActionReference interactionAction;
    
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
        
        generatorText.GetComponent<Text>().text = "Jerry Can " + gasAmountPlayer + "/" + gasAmountNeed;
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
        if (playerInzone)
        {
            
        }
    }
    
}
