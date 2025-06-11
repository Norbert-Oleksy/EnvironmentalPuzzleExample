using UnityEngine;

namespace AE
{
    public class PlayerCameraController : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Camera _camera;
        [SerializeField] private float _sensitivity = 1.0f;
        #endregion

        #region Variables
        private InputManager _iManager;
        private float _baseRotation = 0;
        #endregion

        #region Logic
        private void PlayerRotationLogic()
        {
            float rotationX = _iManager.rotatingVector.x * _sensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationX);
        }

        private void CameraRotationLogic()
        {
            float rotationY = _iManager.rotatingVector.y * _sensitivity * Time.deltaTime;
            _baseRotation -= rotationY;
            _baseRotation = Mathf.Clamp(_baseRotation, -90f, 90f);

            _camera.transform.localRotation = Quaternion.Euler(_baseRotation, 0f, 0f);
        }
        #endregion

        #region Unity-API
        private void Awake()
        {
            _iManager = FindFirstObjectByType<InputManager>();
            if(_camera == null) _camera = GetComponentInChildren<Camera>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            PlayerRotationLogic();
            CameraRotationLogic();
        }
        #endregion
    }
}