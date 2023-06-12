using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.SelectionManagement
{
    /// <summary>
    /// Super basic implementation
    /// </summary>
    public class ExampleGameController : MonoBehaviour
    {
        private SelectionManager selectionManager;
        private void Start()
        {
            selectionManager = FindObjectOfType<SelectionManager>();
            
            List<GameObject> selectables = new List<GameObject>();
            
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.TryGetComponent<ISelectable>(out var temp))
                {
                    selectables.Add(obj);
                }
            }
            //selectionManager.SetSelectableObjects(selectables);
        }
    }
}