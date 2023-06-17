using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.MapCreation
{
    [ExecuteInEditMode]
    public class Map : MonoBehaviour
    {
        [HideInInspector] public List<Vector3> Tiles = new List<Vector3>();
        private Transform wire;

        [SerializeField] private Color wireColor = Color.cyan;
        [SerializeField] private Material groundMaterial;

        private MeshRenderer groundRenderer;
        private MeshRenderer wireRenderer;

        private Dictionary<Vector3, GameObject> tileDictionary = new Dictionary<Vector3, GameObject>();

        private static readonly int WireframeBackColour = Shader.PropertyToID("_WireframeBackColour");
        private static readonly int WireframeAliasing = Shader.PropertyToID("_WireframeAliasing");
        

        private void Start()
        {
            groundRenderer = GetComponent<MeshRenderer>();
            
            wire = transform.GetChild(0);
            wireRenderer = wire.GetComponent<MeshRenderer>(); 
            var wireMaterial = wireRenderer.material = new Material(Shader.Find("DevRowInteractive/WireframeQuad"));
            wireMaterial.SetFloat(WireframeAliasing, 0.5f);
        }

        public void SetTiles(List<Vector3> tiles) => Tiles = tiles;

        public void SetTileOccupied(Vector3 position, GameObject obj)
        {
            tileDictionary.Add(position, obj);
        }

        public GameObject GetTileReferenceAtPosition(Vector3 position)
        {
            foreach (var tile in tileDictionary)
            {
                if (Vector3.Distance(tile.Key ,position) <= 0.1f)
                    return tile.Value;
            }

            return null;
        }

        private void Update()
        {
            // Show map grid
            wire.gameObject.SetActive(MACROS_MAPCREATION.SHOW_MAP_GRID);

            if(groundMaterial)
                groundRenderer.material = groundMaterial;

            wireRenderer.sharedMaterial.SetColor(WireframeBackColour, wireColor);
        }
    }
}