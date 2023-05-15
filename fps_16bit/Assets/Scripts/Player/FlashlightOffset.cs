using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightOffset : MonoBehaviour
{
    private Vector3 vectorOffset;
    private GameObject goFollow;

    [SerializeField] private float speed = 3f;

    
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
