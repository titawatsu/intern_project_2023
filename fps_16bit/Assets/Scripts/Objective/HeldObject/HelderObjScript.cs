using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HelderObjScript : MonoBehaviour
{
    [Header("InteractableInfo")]
    public float sphereCastRadius = 0.5f;
    //public int interactableLayerIndex;
    public LayerMask heldMask;
    private Vector3 raycastPos;
    public GameObject lookObject;
    private ObjectHelder ObjectHelder;
    private Camera mainCamera;

    [Header("Pickup")]
    [SerializeField] private Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;
    private float currentSpeed = 0f;
    private float currentDistance = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRot;

    [SerializeField] private InputActionReference pickupAction;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        pickupAction.action.Enable();
        pickupAction.action.performed += OnPickupActionStarted;
    }

    private void OnDisable()
    {
        pickupAction.action.Disable();
        pickupAction.action.performed -= OnPickupActionStarted;
    }

    private void OnPickupActionStarted(InputAction.CallbackContext context)
    {
        // and we're not holding anything
        if (currentlyPickedUpObject == null)
        {
            // and we are looking at an interactable object
            if (lookObject != null)
            {
                PickUpObject();
            }
        }
        // if we press the pickup button and have something, we drop it
        else
        {
            BreakConnection();
        }
    }

    private void OnPickupActionCanceled(InputAction.CallbackContext context)
    {
        // Or Add any necessary cleanup code or Logic here
        if (currentlyPickedUpObject != null)
        {
            BreakConnection();
        }
    }

    //A simple visualization of the point we're following in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pickupParent.position, 0.5f);
    }

    //Interactable Object detections and distance check
    void Update()
    {
        //Here we check if we're currently looking at an interactable object
        raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hit, maxDistance, heldMask)) //1 << interactableLayerIndex
        {

            lookObject = hit.collider.transform.root.gameObject;

        }
        else
        {
            lookObject = null;
        }
    }

    //Velocity movement toward pickup parent and rotation
    private void FixedUpdate()
    {
        if (currentlyPickedUpObject != null)
        {
            currentDistance = Vector3.Distance(pickupParent.position, pickupRB.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDistance / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = pickupParent.position - pickupRB.position;
            pickupRB.velocity = direction.normalized * currentSpeed;
            //Rotation
            lookRot = Quaternion.LookRotation(mainCamera.transform.position - pickupRB.position);
            lookRot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            pickupRB.MoveRotation(lookRot);
        }

    }

    //Release the object
    public void BreakConnection()
    {
        pickupRB.constraints = RigidbodyConstraints.None;
        currentlyPickedUpObject = null;
        ObjectHelder.pickedUp = false;
        currentDistance = 0;
    }

    public void PickUpObject()
    {
        ObjectHelder = lookObject.GetComponentInChildren<ObjectHelder>();
        currentlyPickedUpObject = lookObject;
        pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
        pickupRB.constraints = RigidbodyConstraints.FreezeRotation;
        ObjectHelder.helderObjScript = this;
        StartCoroutine(ObjectHelder.PickUp());
    }
}
