using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace AE
{
    public class PuzzleObjectsCollector : PuzzlePart
    {
        #region Events
        [SerializeField] private UnityEvent OnQuotaUpdate;
        #endregion

        #region SerializeFields
        [SerializeField] private ColliderLogicDelegate _collector;
        [SerializeField] private List<QuotaEntry> _quota;
        #endregion

        #region Variables
        private Dictionary<string, int> _currentCounts = new();
        #endregion

        #region Logic
        public override bool CheckConditions()
        {
            foreach (var quota in _quota)
            {
                if (!_currentCounts.ContainsKey(quota.tag) || _currentCounts[quota.tag] < quota.required)
                    return false;
            }
            return true;
        }

        private void CollectorLogic(Collider other)
        {
            if(_quota.Exists(q => q.tag == other.tag))
            {
                UpdateQuota(other.tag);
                Destroy(other.gameObject);

                EvaluateState();
            }
            else
            {
                other.transform.position = Vector3.zero;
            }
        }

        private void UpdateQuota(string tag)
        {
            if (_currentCounts.ContainsKey(tag))
                _currentCounts[tag]++;
            else
                _currentCounts[tag] = 1;

            OnQuotaUpdate?.Invoke();
        }

        public override void PrepareThisPart()
        {
            if(_collector == null)
            {
                Debug.LogError($"The {this} is missing _collector reference. Component requier to cellect objects");
                enabled = false;
                return;
            }

            _collector.IsTrigger(true);
            _collector.OnTriggerEnterEvent.AddListener(CollectorLogic);

            _currentCounts.Clear();
        }
        #endregion

        [System.Serializable]
        public struct QuotaEntry
        {
            public string tag;

            [Min(1)]
            public int required;
        }
    }
}