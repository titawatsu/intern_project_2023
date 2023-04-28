using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _playerRigidbody;
        private PlayerInputManager inputManager;


        [SerializeField] private Transform Camera;
        [SerializeField] private Transform CameraRoot;

        [SerializeField] private float UpperLimit = -60f;
        [SerializeField] private float BottomLimit = 50f;
        [SerializeField] private float MouseSensitivity = 60.0f;

        private Animator _animator;
        private bool _hasAnimator;
        private float AnimChangeSpeed = 8.9f;

        private int _xVelHash;
        private int _yVelHash;

        private float _xRotation;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;
        private Vector2 _currentVelocity;

        private void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            inputManager = GetComponent<PlayerInputManager>();


            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
           
        }

        private void FixedUpdate()
        {
            Move();
        }
        private void LateUpdate()
        {
            CamMovements();
        }

        private void CamMovements()
        {
            if (!_hasAnimator) return;

            var Mouse_X = inputManager.Look.x;
            var Mouse_Y = inputManager.Look.y;
            Camera.position = CameraRoot.position;


            _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = inputManager.Sprint ? _runSpeed : _walkSpeed;
            if (inputManager.Move == Vector2.zero) targetSpeed = 0;


            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, inputManager.Move.x * targetSpeed, AnimChangeSpeed * Time.fixedDeltaTime);
            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, inputManager.Move.y * targetSpeed, AnimChangeSpeed * Time.fixedDeltaTime);

            var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
            var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;

            _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);

            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }
    }
}


