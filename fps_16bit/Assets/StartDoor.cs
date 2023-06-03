using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartDoor : MonoBehaviour
{
    private bool playerInZone;                  //Check if the player is in the zone

    [SerializeField] private Animator doorAnim;
    [SerializeField] private Collider doorCollider;

    private void Start()
    {
        
        playerInZone = false;                   //Player not in zone

        doorAnim = transform.parent.gameObject.GetComponent<Animator>();

    }

    private void OnTriggerStay(Collider other)
    {
        playerInZone = true;
        doorAnim.SetBool("open", true);
        doorCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
        doorAnim.SetBool("open", false);
        doorCollider.enabled = true;
    }

}
