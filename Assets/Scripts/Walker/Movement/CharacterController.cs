using System;
using UnityEngine;
using Utility;
using Walker.Controllers;

namespace Walker.Movement
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] 
        private CameraController _cameraController;

        [SerializeField] 
        private Rigidbody _rigidbody;

        [SerializeField] 
        private float _speed;

        private bool _isGameRunning;
        
        private Vector2 _keyboardInput;

        private Vector3 _velocity;

        public Action OnFinished = delegate { };

        private void OnEnable()
        {
            _isGameRunning = true;
        }

        private void Update()
        {
            if (!_isGameRunning)
            {
                return;
            }
            
            _keyboardInput = GetKeyboardInput();
        }

        private void FixedUpdate()
        {
            if (!_isGameRunning)
            {
                return;
            }

            _velocity = GetVelocity();

            _rigidbody.velocity = _velocity;
        }

        private Vector3 GetVelocity()
        {
            var inputVelocity = CalculateInputVelocity();
    
            var (cameraForward, cameraRight) = GetCameraForwardAndRight();

            var relativeForwardPosition = cameraForward * inputVelocity.y;
            
            var relativeRightPosition = cameraRight * inputVelocity.x;
    
            return relativeForwardPosition + relativeRightPosition;
        }

        private Vector3 CalculateInputVelocity()
        {
            return _keyboardInput * _speed;
        }

        private (Vector3 forward, Vector3 right) GetCameraForwardAndRight()
        {
            var (cameraForward, cameraRight) = _cameraController.GetCameraDirections();

            cameraForward.y = cameraRight.y = 0;

            return (cameraForward.normalized, cameraRight.normalized);
        }

        private static Vector2 GetKeyboardInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(RuntimeConstants.WalkerFinishLayerName))
            {
                return;
            }

            _isGameRunning = false;

            _rigidbody.velocity = Vector3.zero;
            
            OnFinished?.Invoke();
        }
    }
}