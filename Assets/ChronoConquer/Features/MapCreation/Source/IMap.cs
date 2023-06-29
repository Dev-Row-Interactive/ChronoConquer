using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.MapCreation
{
    public interface IMap
    {
        public void SetTiles(List<Vector3> tiles);
        public List<Vector3> GetTiles();
        public void SetTileOccupied(Vector3 position, GameObject obj);
        public GameObject GetTileReferenceAtPosition(Vector3 position);
    }
}