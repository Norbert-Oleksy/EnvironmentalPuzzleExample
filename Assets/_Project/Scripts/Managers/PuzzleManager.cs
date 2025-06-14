using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public class PuzzleManager : MonoBehaviour
    {
        #region Events
        public UnityEvent onPuzzleComplete;
        #endregion

        #region Variables
        private List<PuzzlePart> _parts;
        #endregion

        #region Logic
        public void AddPartOfThePuzzle(PuzzlePart part)
        {
            if (!_parts.Contains(part)) _parts.Add(part);
        }

        public void CheckPuzzleState()
        {
            if (IsComplete()) PuzzleSolved();
        }

        private bool IsComplete()
        {
            foreach (var part in _parts)
                if(!part.IsSolved) return false;

            return true;
        }

        private void PuzzleSolved()
        {
            onPuzzleComplete?.Invoke();
        }
        #endregion

        #region Unity-API
        private void Awake()
        {
            _parts = FindObjectsOfType<PuzzlePart>().ToList();
        }
        #endregion
    }
}