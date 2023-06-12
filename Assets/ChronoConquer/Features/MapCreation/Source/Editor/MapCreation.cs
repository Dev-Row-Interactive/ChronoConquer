using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DevRowInteractive.MapCreation.Editor
{
    public class MapCreation : EditorWindow
    {
        private int tileSize = 10;
        private List<Vector3> tiles;

        [MenuItem("Dev Row Interactive/Map Creation")]
        public static void ShowWindowStaggeredMap()
        {
            GetWindow<MapCreation>("Map Creation");
        }

        private void OnGUI()
        {
            tileSize = EditorGUILayout.IntField("Size", tileSize);

            if (GUILayout.Button("Create Staggered Map at 45 degree angle"))
            {
                CreateTilePatternAtAngle();
            }

            if (GUILayout.Button("Create Staggered Map without angle"))
            {
                CreateTilePatternWithoutAngle();
            }
        }


        /// <summary>
        /// Creates a map with the tiles being rotated to 45 degrees. The camera rotation has to be 0
        /// </summary>
        private void CreateTilePatternAtAngle()
        {
            // Create a new GameObject to hold the tile pattern
            GameObject patternObject = new GameObject("Map_Isometric");

            // Attach a MeshFilter component to the GameObject
            MeshFilter meshFilter = patternObject.AddComponent<MeshFilter>();

            // Attach a MeshRenderer component to the GameObject
            MeshRenderer meshRenderer = patternObject.AddComponent<MeshRenderer>();

            // Create a new mesh for the tile pattern
            Mesh patternMesh = new Mesh();

            int rowCount = tileSize;
            int columnCount = tileSize;

            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            int vIndex = 0;
            int tIndex = 0;

            for (int x = 0; x < rowCount; x++)
            {
                for (int z = 0; z < columnCount; z++)
                {
                    float offset = 0.5f;
                    float xPos = x - rowCount * offset;
                    float zPos = z - columnCount * offset;


                    //is even
                    if (z % 2 == 0)
                    {
                        // Define the vertices of the rhombus pattern for each tile
                        vertices.Add(new Vector3(xPos - 0.5f, 0f, zPos / 2));
                        vertices.Add(new Vector3(xPos, 0f, zPos / 2 + 0.5f));
                        vertices.Add(new Vector3(xPos + 0.5f, 0f, zPos / 2));
                        vertices.Add(new Vector3(xPos, 0f, zPos / 2 - 0.5f));

                        // Define the triangles of the rhombus pattern for each tile
                        triangles.Add(vIndex);
                        triangles.Add(vIndex + 1);
                        triangles.Add(vIndex + 2);
                        triangles.Add(vIndex + 2);
                        triangles.Add(vIndex + 3);
                        triangles.Add(vIndex);
                    }

                    //is odd
                    else
                    {
                        // Define the vertices of the rhombus pattern for each tile
                        vertices.Add(new Vector3(xPos - 0.5f + offset, 0f, zPos / 2));
                        vertices.Add(new Vector3(xPos + offset, 0f, zPos / 2 + 0.5f));
                        vertices.Add(new Vector3(xPos + 0.5f + offset, 0f, zPos / 2));
                        vertices.Add(new Vector3(xPos + offset, 0f, zPos / 2 - 0.5f));

                        // Define the triangles of the rhombus pattern for each tile
                        triangles.Add(vIndex);
                        triangles.Add(vIndex + 1);
                        triangles.Add(vIndex + 2);
                        triangles.Add(vIndex + 2);
                        triangles.Add(vIndex + 3);
                        triangles.Add(vIndex);
                    }

                    vIndex += 4;
                    tIndex += 6;
                }
            }
            
            CreateMap(patternMesh, vertices, triangles, meshFilter, meshRenderer, patternObject, rowCount);
        }

        /// <summary>
        /// Creates a map with the tiles not being rotated. The camera rotation has to be 45
        /// </summary>
        private void CreateTilePatternWithoutAngle()
        {
            tiles = new List<Vector3>();
        
            // Create a new GameObject to hold the tile pattern
            GameObject patternObject = new GameObject("Map");

            // Attach a MeshFilter component to the GameObject
            MeshFilter meshFilter = patternObject.AddComponent<MeshFilter>();

            // Attach a MeshRenderer component to the GameObject
            MeshRenderer meshRenderer = patternObject.AddComponent<MeshRenderer>();

            // Create a new mesh for the tile pattern
            Mesh patternMesh = new Mesh();


            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();


            int rowCount = tileSize;

            int initialColCount = 1;
            int colCount = initialColCount;

            int vIndex = 0;
            int tIndex = 0;
        
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    int offset = colCount / 2;
                    
                    vertices.Add(new Vector3(col + -0.5f - offset, 0, row + 0.5f));
                    vertices.Add(new Vector3(col + 0.5f - offset, 0, row + 0.5f));
                    vertices.Add(new Vector3(col + 0.5f - offset, 0, row + -0.5f));
                    vertices.Add(new Vector3(col + -0.5f - offset, 0, row + -0.5f));
                
                    triangles.Add(vIndex);
                    triangles.Add(vIndex + 1);
                    triangles.Add(vIndex + 2);
                    triangles.Add(vIndex + 2);
                    triangles.Add(vIndex + 3);
                    triangles.Add(vIndex);

                    vIndex += 4;
                    tIndex += 6;

                    Vector3 tile = new Vector3(col - offset, 0, row);
                    tiles.Add(tile);
                }

                if (row >= (rowCount / 2) - 1)
                    colCount -= 2;
                else
                    colCount += 2;
            }

            CreateMap(patternMesh, vertices, triangles, meshFilter, meshRenderer, patternObject, rowCount);
        }

        private void CreateMap(Mesh patternMesh, List<Vector3> vertices, List<int> triangles, MeshFilter meshFilter,
            MeshRenderer meshRenderer, GameObject mapObject, int rowCount)
        {
            // Assign the vertices and triangles to the mesh
            patternMesh.SetVertices(vertices);
            patternMesh.SetTriangles(triangles, 0);

            // Calculate the normals for proper lighting
            patternMesh.RecalculateNormals();
            
            string savePath = EditorUtility.SaveFilePanelInProject("Save Mesh", "GeneratedMesh", "asset", "Save mesh as asset");
            if (string.IsNullOrEmpty(savePath))
                return;

            AssetDatabase.CreateAsset(patternMesh, savePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Assign the mesh to the MeshFilter component
            meshFilter.mesh = patternMesh;
            
            var offsetPos = new Vector3(0, 0, (-rowCount / 2f) + 1);
            mapObject.transform.position = offsetPos;

            var wireObject = Instantiate(mapObject, mapObject.transform.position, mapObject.transform.rotation);
            wireObject.name = mapObject.name + "_Wire";
            wireObject.transform.SetParent(mapObject.transform);

            mapObject.AddComponent<MeshCollider>();
            mapObject.layer = 3;

            var map = mapObject.AddComponent<Map>();
            map.SetTiles(MapHelpers.GetWorldPositions(tiles, mapObject.transform));
        }
    }
}