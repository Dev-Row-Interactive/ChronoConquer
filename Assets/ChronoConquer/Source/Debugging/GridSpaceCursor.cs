using DevRowInteractive.ChronoConquer.Source.Core;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Debugging
{
    public class WorldSpaceCursor : MonoBehaviour
    {
        private void Update() => SetPosition(Helpers.HelperMaths.FindNearest(GameManager.Instance.SelectionManager.GetWorldMousePosition(), GameManager.Instance.Map.Tiles));
        private void SetPosition(Vector3 position) => transform.position = position;
    }
}