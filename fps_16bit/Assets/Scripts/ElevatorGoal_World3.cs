using System.Collections;
using System.Collections.Generic;
using fps_16bit;
using UnityEngine;

public class ElevatorGoal_World3 : MonoBehaviour
{
    public Animator elevatorAnim;
    public bool playerInzone = false;
    
    private void Start()
    {
        elevatorAnim = transform.parent.gameObject.GetComponent<Animator>();
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
