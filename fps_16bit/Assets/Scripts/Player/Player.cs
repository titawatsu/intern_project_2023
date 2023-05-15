using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public static Player instance;
    
    public int playerHealth;

    public Text playerHealthText;

    public int gasAmount = 0;

    private void Start()
    {
        gasAmount = 0;
    }

    private void Awake()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        playerHealthText.text = $"HP:{playerHealth}";
    }

    public void IncreaseHealth(int value)
    {
        playerHealth += value;
        
    }

    public void IncreaseGasAmount(int value)
    {
        gasAmount += value;
    }
    
}
