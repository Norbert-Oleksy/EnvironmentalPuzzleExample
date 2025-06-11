using UnityEngine;

namespace AE
{
    [RequireComponent (typeof (CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private float _gravity = -9.81f;
        #endregion

        #region Variables
        private CharacterController _controller;
        private InputManager _iManager;
        #endregion

        #region Logic
        private void MovementLogic()
        {
            Vector3 move = transform.right * _iManager.movementVector.x + transform.forward * _iManager.movementVector.y;
            _controller.Move(move * _speed * Time.deltaTime);
        }

        private void GravityLogic()
        {
            Vector3 velocity = new();
            velocity.y = -8f + _gravity * Time.deltaTime;
            _controller.Move(velocity * Time.deltaTime);
        }
        #endregion

        #region Unity-API
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _iManager = FindFirstObjectByType<InputManager>();
        }

        private void Update()
        {
            MovementLogic();
            GravityLogic();
        }
        #endregion
    }
}