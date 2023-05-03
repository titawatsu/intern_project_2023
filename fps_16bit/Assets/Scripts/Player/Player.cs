using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    public int playerHealth;

    public Text playerHealthText;

    private void Awake()
    {
        instance = this;
    }

    public void IncreaseHealth(int value)
    {
        playerHealth += value;
        playerHealthText.text = $"HP:{playerHealth}";
    }
    
}
