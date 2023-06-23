using DevRowInteractive.SelectionManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public abstract class PanelMovement : MonoBehaviour
    {
        private Vector3 endPos;
        private Vector3 startPos;

        private RectTransform rectTransform;

        private bool isIn = true;

        public virtual void Awake() => TryGetComponent(out rectTransform);

        public virtual void Start()
        {
            endPos = rectTransform.anchoredPosition;
            startPos = new Vector3(rectTransform.anchoredPosition.x, -500);
            MovePanelOut();
        }

        public virtual void MovePanelIn(ISelectable selectable = null)
        {
            if (isIn)
                return;
            LeanTween.move(rectTransform, endPos, MACROS_UI.ANIMATION_TIME);

            isIn = !isIn;
        }

        public virtual void MovePanelOut(ISelectable selectable = null)
        {
            if (!isIn)
                return;

            LeanTween.move(rectTransform, startPos, MACROS_UI.ANIMATION_TIME);

            isIn = !isIn;
        }
    }
}