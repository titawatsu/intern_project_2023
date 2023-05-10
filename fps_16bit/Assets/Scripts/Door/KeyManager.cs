using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public BoxCollider keyCollider;

    public Text txtKey;
    public DoorManager doorManager;

    private void Start()
    {
        keyCollider = GetComponent<BoxCollider>();
        keyCollider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorManager.gotKey = true;
            txtKey.gameObject.SetActive(true);
            txtKey.text = "Key Acquired";
            this.gameObject.SetActive(false);
        }
    }
}
