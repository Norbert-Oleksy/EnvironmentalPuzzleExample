using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE
{
    public class InputManager : MonoBehaviour
    {
        #region InputActions
        private InputActionsSystemAsset _inputs;
        private InputAction _movement;
        private InputAction _rotating;
        private InputAction _interact;
        #endregion

        #region Variables
        public Vector2 movementVector { get; private set; }
        public Vector2 rotatingVector { get; private set; }
        #endregion

        #region Events
        public event Action OnInteraction;
        #endregion

        #region Logic
        private void OnPlayerMovement() => movementVector = _movement.ReadValue<Vector2>();
        private void OnCameraRotating() => rotatingVector = _rotating.ReadValue<Vector2>();
        #endregion

        #region Unity-API
        private void Awake()
        {
            _inputs = new InputActionsSystemAsset();

            _movement = _inputs.Player.Move;
            _movement.performed += _ => OnPlayerMovement();
            _movement.canceled += _ => movementVector = Vector2.zero;

            _rotating = _inputs.Player.Look;
            _rotating.performed += _ => OnCameraRotating();
            _rotating.canceled += _ => rotatingVector = Vector2.zero;

            _interact = _inputs.Player.Interact;
            _interact.started += _ => OnInteraction?.Invoke();
        }

        private void OnEnable()
        {
            _movement.Enable();
            _rotating.Enable();
            _interact.Enable();
        }

        private void OnDisable()
        {
            _movement.Disable();
            _rotating.Disable();
            _interact.Disable();
        }
        #endregion
    }
}