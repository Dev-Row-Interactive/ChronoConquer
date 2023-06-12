using UnityEngine;

namespace DevRowInteractive.SelectionManagement
{
    /// <summary>
    /// Displays a reference point in world-space for debugging
    /// </summary>
    public class WorldSpaceCursor : MonoBehaviour
    {
        private SelectionManager selectionManager;
        private void Start() => selectionManager = FindObjectOfType<SelectionManager>();
        private void Update() => SetPosition(selectionManager.GetWorldMousePosition());
        private void SetPosition(Vector3 position) => transform.position = position;
    }
}