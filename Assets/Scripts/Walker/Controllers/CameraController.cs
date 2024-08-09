using UnityEngine;

namespace Walker.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        [SerializeField] private Transform _target;
        
        [SerializeField, Range(3f, 10f)] 
        private float _distance = 5.0f;
        
        [SerializeField, Range(1f, 3f)] 
        private float _sensitivity = 2.0f;

        private const float MaxYAngle = 70f;

        private Vector2 _currentRotation;

        private Vector2 _mouseInput;

        public (Vector3 forward, Vector3 right) GetCameraDirections()
        {
            var cameraTransform = _mainCamera.transform;
            
            return (cameraTransform.forward, cameraTransform.right);
        }

        private void OnEnable()
        {
            var eulerAngles = transform.eulerAngles;
            
            _currentRotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }

        private void Update()
        {
            _mouseInput = GetMouseInput();
        }

        private void LateUpdate()
        {
            UpdateRotation();
            
            UpdatePosition();
            
            LookAtTarget();
        }

        private void UpdateRotation()
        {
            _currentRotation.x += _mouseInput.x * _sensitivity;
            
            _currentRotation.y -= _mouseInput.y * _sensitivity;
            
            _currentRotation.y = Mathf.Clamp(_currentRotation.y, -MaxYAngle, MaxYAngle);
        }

        private void UpdatePosition()
        {
            var rotation = GetRotation();
            
            transform.position = GetCameraPosition(rotation);
        }

        private Quaternion GetRotation()
        {
            return Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0);
        }

        private void LookAtTarget()
        {
            transform.LookAt(_target);
        }

        private Vector3 GetCameraPosition(Quaternion rotation)
        {
            return _target.position - rotation * Vector3.forward * _distance;
        }

        private Vector2 GetMouseInput()
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}