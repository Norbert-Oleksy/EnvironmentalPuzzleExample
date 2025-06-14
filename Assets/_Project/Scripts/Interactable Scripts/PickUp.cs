using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

namespace AE
{
    [RequireComponent(typeof(Rigidbody))]
    public class PickUp : MonoBehaviour, IInteractable
    {
        #region Interaction
        [SerializeField]
        private bool _isInteractable = true;
        public bool IsInteractable { get => _isInteractable; private set => _isInteractable = value; }

        public void Interact(Transform initiator)
        {
            if(_holder == null)
            {
                _holder = initiator;
                PickUpObject();
            }
            else
            {
                DropObject();
            }

        }
        #endregion

        #region Variables
        private Rigidbody _rb;
        private Transform _holder;
        #endregion

        #region Logic
        public void PickUpObject()
        {
            Transform newParent = _holder.GetComponent<PlayerInteractionController>().PickUpObject(this);

            _rb.isKinematic = true;
            _rb.useGravity = false;

            transform.SetParent(newParent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void DropObject()
        {
            _holder = null;
            transform.SetParent(null);
            _rb.isKinematic = false;
            _rb.useGravity = true;
        }
        #endregion

        #region Unity-API
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        #endregion
    }
}