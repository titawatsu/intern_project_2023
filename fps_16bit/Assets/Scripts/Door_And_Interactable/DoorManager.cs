using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DoorManager : MonoBehaviour
{
    public bool keyNeeded = false;              
    public bool gotKey;                 
    public GameObject keyGameObject; //If player has Key,  assign it here
    
    public Text txtToDisplay;

    public DoorState State { get; internal set; }

    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him
    
    public InputActionReference doorAction;

    public enum DoorState
    {
        Closed,
        Opened,
        Jammed
    }

    DoorState doorState = new DoorState();      //To check the current state of the door
    
    private void OnEnable()
    {
        doorAction.action.Enable();
        doorAction.action.performed += DoorInteractHandle;
    }

    private void OnDisable()
    {
        doorAction.action.Disable();
        doorAction.action.performed -= DoorInteractHandle;
    }
    
    private void Start()
    {
        gotKey = false;
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        doorState = DoorState.Closed;           //Starting state is door closed

        txtToDisplay.enabled = false;

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

        //If Key is needed and the KeyGameObject is not assigned, stop playing and throw error
        if (keyNeeded && keyGameObject == null)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("Assign Key GameObject");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        txtToDisplay.enabled = true;
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
        txtToDisplay.enabled = false;
    }

    private void Update()
    {
        if (playerInZone)
        {
            if (doorState == DoorState.Opened)
            {
                txtToDisplay.GetComponent<Text>().text = "Press 'E' to Close";
                doorCollider.enabled = false;
            }
            else if (doorState == DoorState.Closed || gotKey)
            {
                txtToDisplay.GetComponent<Text>().text = "Press 'E' to Open";
                doorCollider.enabled = true;
            }
            else if (doorState == DoorState.Jammed)
            {
                txtToDisplay.GetComponent<Text>().text = "Needs Key";
                doorCollider.enabled = true;
            }
        }

    }
    
    void DoorInteractHandle(InputAction.CallbackContext context)
    {
        if (playerInZone)
        {
            doorOpened = !doorOpened;           //The toggle function of door to open/close
    
            if (doorState == DoorState.Closed && !doorAnim.isPlaying)
            {
                if (!keyNeeded)
                {
                    doorAnim.Play("Door_Open");
                    doorState = DoorState.Opened;
                }
                else if (keyNeeded && !gotKey)
                {
                    if (doorAnim.GetClip("Door_Jam") != null)
                        doorAnim.Play("Door_Jam");
                    doorState = DoorState.Jammed;
                }
            }
    
            if (doorState == DoorState.Closed && gotKey && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }
    
            if (doorState == DoorState.Opened && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Close");
                doorState = DoorState.Closed;
            }
    
            if (doorState == DoorState.Jammed && !gotKey)
            {
                if (doorAnim.GetClip("Door_Jam") != null)
                    doorAnim.Play("Door_Jam");
                doorState = DoorState.Jammed;
            }
            else if (doorState == DoorState.Jammed && gotKey && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }
        }
    }
}