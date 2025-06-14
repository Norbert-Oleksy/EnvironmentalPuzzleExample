using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public class Lever : MonoBehaviour, IInteractable
    {
        #region SerializeFields
        [Header("Lever")]
        [SerializeField] private Transform _leverObject;
        [SerializeField] private Transform _positionON;
        [SerializeField] private Transform _positionOFF;
        [Space(10.0f)]
        #endregion

        #region Interaction
        [SerializeField]
        private bool _isInteractable = true;
        public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }

        public void Interact(Transform initiator)
        {
            Switch();
        }
        #endregion

        #region Variables
        public UnityEvent onStateChange;
        public bool state;
        #endregion

        #region Logic
        private void Switch()
        {
            state = !state;
            ChangeLeverPosition();
            onStateChange?.Invoke();
        }

        private void ChangeLeverPosition()
        {
            _leverObject.position = state ? _positionON.position : _positionOFF.position;
            _leverObject.rotation = state ? _positionON.rotation : _positionOFF.rotation;
        }
        #endregion

        #region Unity-API
        private void Awake()
        {
            ChangeLeverPosition();
        }
        #endregion
    }
}