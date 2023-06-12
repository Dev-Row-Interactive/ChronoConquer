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
        
        private static readonly int WireframeBackColour = Shader.PropertyToID("_WireframeBackColour");

        private void Start()
        {
            groundRenderer = GetComponent<MeshRenderer>();
            
            wire = transform.GetChild(0);
            wireRenderer = wire.GetComponent<MeshRenderer>(); 
            wireRenderer.material = new Material(Shader.Find("DevRowInteractive/WireframeQuad"));
        }

        public void SetTiles(List<Vector3> tiles) => Tiles = tiles;

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