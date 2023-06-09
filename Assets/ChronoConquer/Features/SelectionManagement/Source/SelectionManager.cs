using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DevRowInteractive.SelectionManagement
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Color rectangleColor = new Color(0.5f, 1f, 0.4f, 0.2f);

        private Vector3 offset = new Vector3(-3.5f, 5, -3.5f);
        private Vector2 screenMousePosition;
        private Vector3 worldMousePosition;
        private Vector2 selectionStartPosition;
        private Vector2 selectionEndPosition;
        private List<ISelectable> currentlySelected = new List<ISelectable>();
        private List<ISelectable> currentlyHovered = new List<ISelectable>();
        private bool isSelecting;
        private Rect selectionRect;
        private Camera camera = new Camera();

        private List<GameObject> selectableObjects = new List<GameObject>();
        public void SetSelectableObjects(List<GameObject> objects) => selectableObjects = objects;
        public void AddSelectableObject(GameObject obj) => selectableObjects.Add(obj);

        private void Start()
        {
            if (camera == null)
                camera = Camera.main;
        }

        private void Update()
        {
            if (selectableObjects == null || selectableObjects.Count == 0)
                Debug.LogWarning("Please provide SelectableObjects that implement the ISelectable Interface");

            DoRaycast();
        }

        private void DoRaycast()
        {
            screenMousePosition = Mouse.current.position.ReadValue();
            worldMousePosition = camera.ScreenToWorldPoint(screenMousePosition) + offset;

            if (Mouse.current.leftButton.wasPressedThisFrame)
                selectionStartPosition = screenMousePosition;

            selectionEndPosition = screenMousePosition;

            if (isSelecting)
                HandleRectangleSelection();

            Ray ray = camera.ScreenPointToRay(screenMousePosition);
            RaycastHit hit;
            
            if(!isSelecting)
                ClearHover();

            if (Physics.Raycast(ray, out hit))
            {
                worldMousePosition = hit.point;
                if (hit.transform.TryGetComponent(out ISelectable selectable))
                {
                    if (!currentlyHovered.Contains(selectable))
                        HandleHover(selectable);

                    if (Mouse.current.leftButton.wasPressedThisFrame && !currentlySelected.Contains(selectable))
                        HandleSelection(selectable);
                }
                else if (Mouse.current.leftButton.wasPressedThisFrame)
                    ClearSelection();
            }
            else if (Mouse.current.leftButton.wasPressedThisFrame)
                ClearSelection();

            isSelecting = Mouse.current.leftButton.isPressed;
        }

        private void HandleRectangleSelection()
        {
            Bounds selectionBounds =
                SelectionHelpers.GetViewportBounds(camera, selectionStartPosition, screenMousePosition);
            
            foreach (GameObject obj in selectableObjects)
            {
                Debug.LogError("SELECTABLE OBJECTS OVER 0");
                if (selectionBounds.Contains(camera.WorldToViewportPoint(obj.transform.position)))
                {
                    obj.TryGetComponent<ISelectable>(out var selectable);
                    
                    if (selectable.IsMultiSelect())
                    {
                        selectable.Hover();
                        currentlyHovered.Add(selectable);
                    }
                    
                    if (Mouse.current.leftButton.wasReleasedThisFrame)
                    {
                        ClearHover();

                        if (selectable.IsMultiSelect())
                        {
                            selectable.Select();
                            currentlySelected.Add(selectable);
                        }
                    }
                }
                else
                {
                    obj.TryGetComponent<ISelectable>(out var selectable);
                    selectable.EndHover();
                    currentlyHovered.Remove(selectable);
                }
            }
        }

        private void HandleSelection(ISelectable selectable)
        {
            ClearSelection();
            selectable.Select();
            currentlySelected.Add(selectable);
        }

        private void HandleHover(ISelectable selectable)
        {
            selectable.Hover();
            currentlyHovered.Add(selectable);
        }

        private void ClearHover()
        {
            foreach (ISelectable selectable in currentlyHovered)
            {
                selectable.EndHover();
            }

            currentlyHovered.Clear();
        }

        private void ClearSelection()
        {
            foreach (ISelectable selectable in currentlySelected)
            {
                selectable.DeSelect();
            }

            currentlySelected.Clear();
        }

        private void OnGUI()
        {
            if (isSelecting)
            {
                selectionRect = SelectionHelpers.GetScreenRect(selectionStartPosition, selectionEndPosition);
                SelectionHelpers.DrawScreenRect(selectionRect, rectangleColor);
                SelectionHelpers.DrawScreenRectBorder(selectionRect, 1, rectangleColor);
            }
        }

        public Vector3 GetWorldMousePosition()
        {
            return worldMousePosition;
        }

        public void SetCamera(Camera camera) => camera = camera;

        public List<GameObject> GetSelectedObjects()
        {
            List<GameObject> selected = new List<GameObject>();

            foreach (var selectable in currentlySelected)
            {
                selected.Add(selectable.GetGameObjectReference());
            }

            return selected;
        }

        public GameObject GetCurrentHover()
        {
            if (currentlyHovered.Count > 0)
                return currentlyHovered[0].GetGameObjectReference();
            return null;
        }
    }
}