using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FlashlightOffset : MonoBehaviour
{
    private Vector3 vectorOffset;
    private GameObject goFollow;

    [SerializeField] private float speed = 3f;

    [SerializeField] private InputActionReference flashlightActionReference;

    private void OnEnable()
    {
        flashlightActionReference.action.Enable();
        flashlightActionReference.action.performed += flashlightHandle;        
    }

    private void OnDisable()
    {
        flashlightActionReference.action.Disable();
        flashlightActionReference.action.performed -= flashlightHandle; 
    }

    private void Start()
    {
        goFollow = Camera.main.gameObject;
        vectorOffset = transform.position - goFollow.transform.position;
    }

    private void Update()
    {
        transform.position = goFollow.transform.position + vectorOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
    }
}
