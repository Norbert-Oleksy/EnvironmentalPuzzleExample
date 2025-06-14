using UnityEngine;

namespace AE
{
    public class PlayerInteractionController : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _pickUpPosition;
        [Header("Ray Settings")]
        [SerializeField] private float _rayDistance = 1.0f;
        [SerializeField] private LayerMask _interactableLayer;
        #endregion

        #region Variables
        private PickUp _heldObject;
        public bool isHolding => _heldObject != null;
        private IInteractable _object;
        private PlayerUI _playerUI;
        #endregion

        #region Logic
        private void CheckForInteraction()
        {
            if(isHolding) return;

            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            RaycastHit rayHit;

            if(Physics.SphereCast(
                ray,
                0.5f,
                out rayHit,
                _rayDistance,
                _interactableLayer))
            {
                if (_object == null) _playerUI.SetCursorType(CursorType.interaction);

                _object = rayHit.transform.GetComponent<IInteractable>();
            }
            else
            {
                if(_object != null) _playerUI.SetCursorType(CursorType.basic);

                _object = null;
            }
        }

        private void HandleInteraction()
        {
            if (isHolding)
                DropHeldObject();
            else
                InteractWithObject();
        }

        private void InteractWithObject()
        {
            if (_object == null
                || !_object.IsInteractable
                || isHolding) return;

            _object.Interact(transform);
        }

        public Transform PickUpObject(PickUp target)
        {
            _heldObject = target;
            target.Interact(_pickUpPosition);

            _playerUI.SetCursorType(CursorType.hide);

            return _pickUpPosition;
        }

        private void DropHeldObject()
        {
            _heldObject.DropObject();
            _heldObject = null;

            _playerUI.SetCursorType(CursorType.basic);
        }
        #endregion

        #region Unity-API
        private void Awake()
        {
            if(_camera == null) _camera = GetComponentInChildren<Camera>();
            _playerUI = FindFirstObjectByType<PlayerUI>();

            FindFirstObjectByType<InputManager>().OnInteraction += HandleInteraction;
        }

        private void Update()
        {
            CheckForInteraction();
        }
        #endregion
    }
}