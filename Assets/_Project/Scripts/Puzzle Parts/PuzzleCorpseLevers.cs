using UnityEngine;
using System.Collections.Generic;

namespace AE
{
    /// <summary>
    /// Puzzle step that is solved by setting a group of levers to their target states.
    /// The player must toggle each lever to match its expected value.
    /// </summary>
    public class PuzzleCorpseLevers : PuzzlePart
    {
        #region SerializeFields
        [SerializeField] private List<LeverCondition> _levers;
        #endregion

        #region Logic
        public override bool CheckConditions()
        {
            foreach(var l in _levers)
                if (l.lever.state != l.expectedValue) return false;

            return true;
        }

        public override void PrepareThisPart()
        {
            onComplete.AddListener(DisableAllLevers);

            foreach(var l in _levers)
            {
                l.lever.OnStateChange.AddListener(EvaluateState);
            }
        }

        private void DisableAllLevers()
        {
            foreach (var l in _levers)
                l.lever.IsInteractable = false;
        }
        #endregion


        [System.Serializable]
        public struct LeverCondition
        {
            public Lever lever;
            public bool expectedValue;
        }
    }
}