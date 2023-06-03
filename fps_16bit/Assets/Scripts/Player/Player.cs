using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using fps_16bit;

namespace fps_16bit
{
    public class Player : MonoBehaviour
    {
        public static Player instance;

        public int playerHealth;

        public Text playerHealthText;

        public int gasAmount = 0;

        public bool getFlashlight = false;

        private void Start()
        {
            playerHealth = 100;

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
                GameManager.instance.ProcessPlayerDeath();

            }
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void GameOver()
        {
            
            this.gameObject.SetActive(false);
        }
    }

}