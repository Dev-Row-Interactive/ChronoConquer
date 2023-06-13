using DevRowInteractive.SelectionManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer
{
    /// <summary>
    /// This class is an example implementation of the ISelectable interface.
    /// It changes it's layer when hovered or selected (e.g. an outline layer).
    /// Check the Interface itself for a brief description of the methods.
    /// </summary>
    public class SelectionExample : MonoBehaviour, ISelectable
    {
        private int formerLayer;
        private readonly int targetLayer = 6;
        private bool isSelected;

        private void Start() => formerLayer = gameObject.layer;
        public void Select()
        {
            isSelected = true;
            SelectionHelpers.SetLayerRecursively(gameObject, targetLayer);
        }

        public void DeSelect()
        {
            isSelected = false;
            SelectionHelpers.SetLayerRecursively(gameObject, formerLayer);
        }
        
        public void Hover()
        {
            if (isSelected)
                return;

            SelectionHelpers.SetLayerRecursively(gameObject, targetLayer);
        }

        public void EndHover()
        {
            if (isSelected)
                return;

            SelectionHelpers.SetLayerRecursively(gameObject, formerLayer);
        }

        public void Reset()
        {
            //
        }

        public GameObject GetGameObjectReference() => gameObject;
        public bool IsMultiSelect() => true;
    }
}