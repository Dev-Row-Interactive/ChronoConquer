using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.MapCreation
{
    public class MapHelpers : MonoBehaviour
    {
        /// <summary>
        /// Converts local space coordinates in worldspace
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="referenceTransform"></param>
        /// <returns></returns>
        public static List<Vector3> GetWorldPositions(List<Vector3> positions, Transform referenceTransform)
        {
            List<Vector3> worldPositions = new List<Vector3>();

            foreach (Vector3 position in positions)
            {
                Vector3 worldPosition = referenceTransform.TransformPoint(position);
                worldPositions.Add(worldPosition);
            }

            return worldPositions;
        }
        
        /// <summary>
        /// Draws text in scene-view at world-position. Has to be called in OnDrawGizmos or similar
        /// </summary>
        /// <param name="text"></param>
        /// <param name="worldPos"></param>
        /// <param name="colour"></param>
        public static void DrawString(string text, Vector3 worldPos, Color? colour = null)
        {
            UnityEditor.Handles.BeginGUI();
            if (colour.HasValue) GUI.color = colour.Value;
            var view = UnityEditor.SceneView.currentDrawingSceneView;
            Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y),
                text);
            UnityEditor.Handles.EndGUI();
        }
    }
}
