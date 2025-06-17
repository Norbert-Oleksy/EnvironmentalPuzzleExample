using UnityEngine;

namespace AE
{
    public class PrizeChest : MonoBehaviour, IInteractable
    {
        #region Logic
        private void FinishTheShow()
        {
            FindFirstObjectByType<InputManager>().enabled = false;
            FindFirstObjectByType<PlayerUI>().ShowFinishMsg();
        }
        #endregion

        #region Interaction
        private bool _isInteractable = true;
        public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }

        public void Interact(Transform initiator)
        {
            FinishTheShow();
        }
        #endregion
    }
}