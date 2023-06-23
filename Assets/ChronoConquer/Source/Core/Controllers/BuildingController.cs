using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DevRowInteractive.ChronoConquer.Source.Core.Controllers
{
    public class BuildingController : MonoBehaviour
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
                        foreach (var selectedObject in currentlySelectedObjects)
                        {
                            if (selectedObject.TryGetComponent<ProductionBuilding>(out var productionBuilding))
                                productionBuilding.SetRallyPoint(resource.transform.position, resource);
                        }
                    }
                }

                else
                {
                    foreach (var obj in currentlySelectedObjects)
                    {
                        if (obj.TryGetComponent<ProductionBuilding>(out var productionBuilding))
                            productionBuilding.SetRallyPoint(
                                GameManager.Instance.SelectionManager.GetWorldMousePosition());
                    }
                }
            }
        }
    }
}