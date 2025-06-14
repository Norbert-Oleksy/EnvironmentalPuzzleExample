using UnityEngine;
using UnityEngine.UI;

namespace AE
{
    public class PlayerUI : MonoBehaviour
    {
        #region SerializeFields
        [Header("UI Elements")]
        [SerializeField] private Image _cursor;
        #endregion

        #region Logic
        public void SetCursorType(CursorType type)
        {
            Color cursorColor = type switch
            {
                CursorType.interaction => Color.green,
                CursorType.hide => new Color(1f, 1f, 1f, 0f),
                CursorType.basic => Color.white,
                _ => Color.white,
            };

            _cursor.color = cursorColor;
        }
        #endregion
    }

    public enum CursorType
    {
        basic,
        interaction,
        hide
    }
}