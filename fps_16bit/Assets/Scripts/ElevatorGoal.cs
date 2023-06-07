using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace fps_16bit
{
    
public class ElevatorGoal : MonoBehaviour
{
    public Animator elevatorAnim;
    public bool playerInzone = false;

    private void Start()
    {
        elevatorAnim = transform.parent.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInzone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInzone = false;
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(GoalAnim());
    }

    private void CheckPlayer()
    {
        elevatorAnim.SetBool("open", playerInzone);
    }

    IEnumerator GoalAnim()
    {
        yield return new WaitForSeconds(3);

        if (playerInzone)
        {
            elevatorAnim.SetBool("open", false);
            StartCoroutine(NextScene());
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.LoadNextLevel();
    }
}

}