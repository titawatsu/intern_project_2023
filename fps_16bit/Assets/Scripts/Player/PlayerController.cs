using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody playerRb; // playerRigidBody
        private PlayerInputManager inputManager; // Get Player Input

        [SerializeField] private CapsuleCollider playerCollider; // Get player collider

        #region CAMERA SCRIPT
        [SerializeField] private Transform Camera; // player camera
        [SerializeField] private Transform CameraRoot; // player camera position

        [SerializeField] private float UpperLimit = -60f;
        [SerializeField] private float BottomLimit = 60f;
        [SerializeField] private float MouseSensitivity = 50.0f;
        #endregion

        private Animator anim;
        private bool checkAnimator;
        private float AnimChangeSpeed = 8.9f;

        private int xVelHash;
        private int yVelHash;
        private float xRotation;
        private Vector2 currentVelocity;

        [SerializeField] private float playerToGround = 0.8f;
        [SerializeField] private float AirResistance = 0.8f;
        [SerializeField] private LayerMask GroundCheck;
        [SerializeField] private float checkGroundRadius = 0.3f;
        [SerializeField] private bool grounded = false;

        private float jumpForce = 500f;

        private int jumpHash;
        private int groundHash;
        private int fallingHash;
        private int zVelHash;

        private int crouchHash;

        private const float walkSpeed = 2f;
        private const float runSpeed = 6f;
        private const float crouchSpeed = 1.5f;
        private const float standHeight = 1.78f;
        private const float crouchHeight = 0.89f;

        [SerializeField] private GameObject stepLowerRay;
        [SerializeField] private GameObject stepUpperRay;

        [SerializeField] private float stepHeight = 0.3f; 
        [SerializeField] private float stepDamp = 0.1f;

        private void Awake()
        {
            playerRb = GetComponent<Rigidbody>();

            stepUpperRay.transform.position = new Vector3(stepUpperRay.transform.position.x, stepHeight, stepUpperRay.transform.position.z);
        }

        private void Start()
        {
            checkAnimator = TryGetComponent<Animator>(out anim);

            inputManager = GetComponent<PlayerInputManager>();


            xVelHash = Animator.StringToHash("X_Velocity");
            yVelHash = Animator.StringToHash("Y_Velocity");
            zVelHash = Animator.StringToHash("Z_Velocity");

            jumpHash = Animator.StringToHash("Jump");
            groundHash = Animator.StringToHash("Grounded");
            fallingHash = Animator.StringToHash("Falling");
            crouchHash = Animator.StringToHash("Crouch");
        }

        private void FixedUpdate()
        {
            CheckGroundAndCollision();
            MoveHandler();
            JumpHandler();
            CrouchHandle();
        }
        private void LateUpdate()
        {
            MovementCheck();
        }

        private void MovementCheck()
        {
            if (!checkAnimator) return;

            var Mouse_X = inputManager.Look.x;
            var Mouse_Y = inputManager.Look.y;
            Camera.position = CameraRoot.position;


            xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
            xRotation = Mathf.Clamp(xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
        }

        private void MoveHandler()
        {
            if (!checkAnimator) return;

            float targetSpeed = inputManager.Sprint ? runSpeed : walkSpeed;

            if (inputManager.Crouch) targetSpeed = crouchSpeed;
            if (inputManager.Move == Vector2.zero) targetSpeed = 0;

            if (grounded)
            {
                currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputManager.Move.x * targetSpeed, AnimChangeSpeed * Time.fixedDeltaTime);
                currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputManager.Move.y * targetSpeed, AnimChangeSpeed * Time.fixedDeltaTime);

                var xVelDifference = currentVelocity.x - playerRb.velocity.x;
                var zVelDifference = currentVelocity.y - playerRb.velocity.z;

                playerRb.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);

            } 
            else 
            {
                playerRb.AddForce(transform.TransformVector(new Vector3(currentVelocity.x * AirResistance, 0, currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }

            anim.SetFloat(xVelHash, currentVelocity.x);
            anim.SetFloat(yVelHash, currentVelocity.y);
        }

        private void CrouchHandle()
        {
            if (anim.GetBool("Crouch") == true)
            {
                playerCollider.height = crouchHeight;
            }
            else
            {
                playerCollider.height = standHeight;
            }

            anim.SetBool(crouchHash, inputManager.Crouch);
        }

       
        private void JumpHandler()
        {
            if (!checkAnimator) return;
            if (!inputManager.Jump) return;
            if (!grounded) return;

            anim.SetTrigger(jumpHash);
        }

        public void JumpAddForce()
        {
            playerRb.AddForce(-playerRb.velocity.y * Vector3.up, ForceMode.VelocityChange);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.ResetTrigger(jumpHash);
        }

        private void CheckGroundAndCollision()
        {
            if (!checkAnimator) return;

            RaycastHit hitLower;
            if (Physics.Raycast(stepLowerRay.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
            {
                RaycastHit hitUpper;
                if (!Physics.Raycast(stepUpperRay.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
                {
                    playerRb.position -= new Vector3(0f, -stepDamp * Time.deltaTime, 0f);
                }
            }

            RaycastHit hitLower45;
            if (Physics.Raycast(stepLowerRay.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
            {

                RaycastHit hitUpper45;
                if (!Physics.Raycast(stepUpperRay.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
                {
                    playerRb.position -= new Vector3(0f, -stepDamp * Time.deltaTime, 0f);
                }
            }

            RaycastHit hitLowerMinus45;
            if (Physics.Raycast(stepLowerRay.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
            {

                RaycastHit hitUpperMinus45;
                if (!Physics.Raycast(stepUpperRay.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
                {
                    playerRb.position -= new Vector3(0f, -stepDamp * Time.deltaTime, 0f);
                }
            }
            
            RaycastHit hitVectorDown;
            if (Physics.SphereCast(playerRb.worldCenterOfMass, checkGroundRadius, Vector3.down, out hitVectorDown, playerToGround + 0.1f, GroundCheck))
            {
                grounded = true; //Player is on the ground
                SetAnimationGrounding();
                return;
            }
            
            grounded = false;
            anim.SetFloat(zVelHash, playerRb.velocity.y);
            SetAnimationGrounding();
            return;
        }

        private void SetAnimationGrounding()
        {
            anim.SetBool(fallingHash, !grounded);
            anim.SetBool(groundHash, grounded);
        }
    }
}


