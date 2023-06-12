using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using DevRowInteractive.EntityManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DevRowInteractive.ChronoConquer.Source.Core.Controllers
{
    public class UnitController : MonoBehaviour
    {
        private GameObject currentHoveredGameObject;
        private List<GameObject> currentlySelectedObjects;

        private void Update()
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                currentlySelectedObjects = GameManager.Instance.SelectionManager.GetSelectedObjects();
                currentHoveredGameObject = GameManager.Instance.SelectionManager.GetCurrentHover();

                if (currentHoveredGameObject)
                {
                    // Handle Resources
                    if (currentHoveredGameObject.TryGetComponent<Resource>(out var resource))
                    {
                        if (resource.CanBeGathered())
                        {
                            foreach (var selectedObject in currentlySelectedObjects)
                            {
                                if (selectedObject.TryGetComponent<IGathering<Resource>>(out var gathering))
                                    gathering.Gather(resource);
                            }
                        }
                    }
                }

                else
                {
                    ArrangeUnits();
                }
            }
        }
        
        private void ArrangeUnits()
        {
            Vector3 mousePosition = GameManager.Instance.SelectionManager.GetWorldMousePosition();
            List<IMovable> movableObjects = new List<IMovable>();
            
            foreach (var selectedObject in GameManager.Instance.SelectionManager.GetSelectedObjects())
            {
                if (selectedObject.TryGetComponent<IMovable>(out var movable))
                {
                    movableObjects.Add(movable);
                }
            }
            
            if (movableObjects.Count == 1)
            {
                movableObjects[0].MakeMovement(mousePosition);
                return;
            }

            
            int unitsPerRow = movableObjects.Count / 2;  // Assuming an even number of units

            for (int i = 0; i < movableObjects.Count; i++)
            {
                float x = i % unitsPerRow - (unitsPerRow - 1) * 0.5f;
                float z = i / unitsPerRow;

                Vector3 offset = new Vector3(x, 0, z);
                Vector3 targetPosition = mousePosition + offset;

                movableObjects[i].MakeMovement(targetPosition);
            }
        }
    }
}