using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightHandle : MonoBehaviour
{
    [SerializeField] private InputActionReference flashlightActionReference;

    [SerializeField] private GameObject flashlightObject;

    [SerializeField] private bool isFlashlightOn;
    
    private void Start()
    {
        isFlashlightOn = false;
        flashlightObject.SetActive(false);
    }

    private void Update()
    {
        if (isFlashlightOn) flashlightObject.SetActive(true);
        else flashlightObject.SetActive(false);
    }

    private void OnEnable()
    {
        flashlightActionReference.action.Enable();
        flashlightActionReference.action.performed += FlashlightInteract;        
    }

    private void OnDisable()
    {
        flashlightActionReference.action.Disable();
        flashlightActionReference.action.performed -= FlashlightInteract; 
    }
    private void FlashlightInteract(InputAction.CallbackContext context)
    {
        isFlashlightOn = !isFlashlightOn;
    }
}
