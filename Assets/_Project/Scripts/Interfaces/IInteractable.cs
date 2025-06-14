
using UnityEngine;

namespace AE
{
    public interface IInteractable
    {
        public bool IsInteractable { get; }
        public void Interact(Transform initiator);
    }
}