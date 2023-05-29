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

    public bool getFlashlight = false;

    [SerializeField]
    private AttackRadius AttackRadius;

    private Coroutine LookCoroutine;

    private const string ATTACK_TRIGGER = "Attack";

    private void Start()
    {
        gasAmount = 0;
    }

    private void Awake()
    {
        instance = this;

        //AttackRadius.OnAttack += OnAttack;
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

    private void OnAttack(IDamageable Target)
    {
        

        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
    }

    private IEnumerator LookAt(Transform Target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * 2;
            yield return null;
        }

        transform.rotation = lookRotation;
    }

    public void TakeDamage(int Damage)
    {
        playerHealth -= Damage;

        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
