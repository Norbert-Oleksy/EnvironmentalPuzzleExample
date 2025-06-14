using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public abstract class PuzzlePart : MonoBehaviour
    {
        #region Events
        public UnityEvent onComplete;
        #endregion

        #region Variables
        private PuzzleManager _manager;
        private bool _isSolved;
        public bool IsSolved => _isSolved;
        #endregion

        #region Logic
        public void EvaluateState()
        {
            if(!CheckConditions()) return;

            _isSolved = true;
            onComplete?.Invoke();
            _manager.CheckPuzzleState();
        }

        public abstract bool CheckConditions();

        public virtual void PrepareThisPart() {}
        #endregion

        #region Unity-API
        private void Awake()
        {
            _manager = FindFirstObjectByType<PuzzleManager>();
            PrepareThisPart();
        }
        #endregion
    }
}