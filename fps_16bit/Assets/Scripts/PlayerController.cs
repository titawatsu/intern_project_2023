using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody playerRb;
        private PlayerInputManager inputManager;
        [SerializeField] private CapsuleCollider playerCollider;

        [SerializeField] private Transform Camera;
        [SerializeField] private Transform CameraRoot;

        [SerializeField] private float UpperLimit = -60f;
        [SerializeField] private float BottomLimit = 50f;
        [SerializeField] private float MouseSensitivity = 60.0f;

        private Animator anim;
        private bool checkAnimator;
        private float AnimChangeSpeed = 8.9f;

        private int xVelHash;
        private int yVelHash;
        private float _xRotation;
        private Vector2 _currentVelocity;

        [SerializeField] private float thisToGround = 0.8f;
        [SerializeField] private float AirResistance = 0.8f;
        [SerializeField] private LayerMask GroundCheck;
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

        private void Start()
        {
            checkAnimator = TryGetComponent<Animator>(out anim);
            playerRb = GetComponent<Rigidbody>();
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
            CheckGround();
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


            _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
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
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, inputManager.Move.x * targetSpeed, AnimChangeSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, inputManager.Move.y * targetSpeed, AnimChangeSpeed * Time.fixedDeltaTime);

                var xVelDifference = _currentVelocity.x - playerRb.velocity.x;
                var zVelDifference = _currentVelocity.y - playerRb.velocity.z;

                playerRb.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);

            } 
            else 
            {
                playerRb.AddForce(transform.TransformVector(new Vector3(_currentVelocity.x * AirResistance, 0, _currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }

            anim.SetFloat(xVelHash, _currentVelocity.x);
            anim.SetFloat(yVelHash, _currentVelocity.y);
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

        private void CheckGround()
        {
            if (!checkAnimator) return;

            RaycastHit hitInfo;
            if (Physics.Raycast(playerRb.worldCenterOfMass, Vector3.down, out hitInfo, thisToGround + 0.1f, GroundCheck))
            {
                //Grounded
                grounded = true;
                SetAnimationGrounding();
                return;
            }
            //Falling
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


