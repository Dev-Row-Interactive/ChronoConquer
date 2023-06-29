using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.SelectionManagement
{
    public interface ISelectionManager
    {
        public Vector3 GetWorldMousePosition();
        public List<GameObject> GetSelectedObjects();
        public GameObject GetCurrentHover();
        public void AddSelectableObject(GameObject obj);

        public delegate void SelectionEvent(ISelectable selectable);

        public event SelectionEvent OnSelect;
        public event SelectionEvent OnDeSelect;
        public event SelectionEvent OnHover;
        public event SelectionEvent OnDehover;
    }
}