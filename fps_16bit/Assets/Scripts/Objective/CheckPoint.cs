using System;
using System.Collections;
using System.Collections.Generic;
using fps_16bit;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private BoxCollider checkpointTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GotoLevel2());
        }
    }

    IEnumerator GotoLevel2()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.LoadNextLevel();
    }
}
