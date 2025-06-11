using UnityEngine;

namespace AE
{
    public class PlayerInteractionController : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Camera _camera;
        [Header("Ray Settings")]
        [SerializeField] private float _rayDistance = 1.0f;
        [SerializeField] private LayerMask _interactableLayer;
        #endregion

        #region Variables
        public bool isHolding {  get; private set; }
        private IInteractable _object;
        #endregion

        #region Logic
        private void CheckForInteraction()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            RaycastHit rayHit;

            if(Physics.SphereCast(
                ray,
                0.5f,
                out rayHit,
                _rayDistance,
                _interactableLayer))
            {
                _object = rayHit.transform.GetComponent<IInteractable>();
            }
            else
            {
                _object = null;
            }
        }

        private void InteractWithObject()
        {
            if(_object == null
                || isHolding) return;

        }
        #endregion

        #region Unity-API
        private void Awake()
        {
            if(_camera == null) _camera = GetComponentInChildren<Camera>();

            FindFirstObjectByType<InputManager>().OnInteraction += InteractWithObject;
        }

        private void Update()
        {
            CheckForInteraction();
        }
        #endregion
    }
}