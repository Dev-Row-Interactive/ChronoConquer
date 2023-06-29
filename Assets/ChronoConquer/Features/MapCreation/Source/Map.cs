using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.MapCreation
{
    [ExecuteInEditMode]
    public class Map : MonoBehaviour, IMap
    {
        public List<Vector3> Tiles { get; private set; }

        [SerializeField] private Color wireColor = Color.cyan;
        [SerializeField] private Material groundMaterial;

        private Transform wire;
        private MeshRenderer groundRenderer;
        private MeshRenderer wireRenderer;
        private readonly Dictionary<Vector3, GameObject> tileDictionary = new Dictionary<Vector3, GameObject>();
        private static readonly int WireframeBackColour = Shader.PropertyToID("_WireframeBackColour");
        private static readonly int WireframeAliasing = Shader.PropertyToID("_WireframeAliasing");


        #region Unity Event Methods

        private void Start()
        {
            groundRenderer = GetComponent<MeshRenderer>();
            Tiles = new List<Vector3>();

            wire = transform.GetChild(0);
            wireRenderer = wire.GetComponent<MeshRenderer>();
            var wireMaterial = wireRenderer.material = new Material(Shader.Find("DevRowInteractive/WireframeQuad"));
            wireMaterial.SetFloat(WireframeAliasing, 0.5f);
        }

        private void Update()
        {
            // Show map grid
            wire.gameObject.SetActive(MACROS_MAPCREATION.SHOW_MAP_GRID);

            if (groundMaterial)
                groundRenderer.material = groundMaterial;

            wireRenderer.sharedMaterial.SetColor(WireframeBackColour, wireColor);
        }

        #endregion

        #region Public Methods

        public void SetTiles(List<Vector3> tiles) => Tiles = tiles;

        public void SetTileOccupied(Vector3 position, GameObject obj) => tileDictionary.Add(position, obj);

        #endregion

        #region Public Getters

        public GameObject GetTileReferenceAtPosition(Vector3 position)
        {
            foreach (var tile in tileDictionary)
            {
                if (Vector3.Distance(tile.Key, position) <= 0.1f)
                    return tile.Value;
            }

            return null;
        }

        public List<Vector3> GetTiles() => Tiles;
        
        #endregion
    }
}