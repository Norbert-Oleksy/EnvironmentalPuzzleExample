using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    [RequireComponent(typeof(Collider))]
    public class ColliderLogicDelegate : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Collider _colider;
        #endregion

        #region Events
        public UnityEvent<Collision> OnCollisionEnterEvent
            , OnCollisionStayEvent
            , OnCollisionExitEvent;
        public UnityEvent<Collider> OnTriggerEnterEvent
            , OnTriggerStayEvent
            , OnTriggerExitEvent;
        #endregion

        #region Logic
        public void IsTrigger(bool value) => _colider.isTrigger = value;
        #endregion

        #region Colider-API
        #region Collision
        private void OnCollisionEnter(Collision collision) => OnCollisionEnterEvent.Invoke(collision);
        private void OnCollisionStay(Collision collision) => OnCollisionStayEvent.Invoke(collision);
        private void OnCollisionExit(Collision collision) => OnCollisionExitEvent.Invoke(collision);
        #endregion

        #region Triger
        private void OnTriggerEnter(Collider other) => OnTriggerEnterEvent.Invoke(other);
        private void OnTriggerStay(Collider other) => OnTriggerStayEvent.Invoke(other);
        private void OnTriggerExit(Collider other) => OnTriggerExitEvent.Invoke(other);
        #endregion
        #endregion

        #region Unity-API
        private void Awake()
        {
            if(_colider == null) _colider = GetComponent<Collider>();
        }
        #endregion
    }
}