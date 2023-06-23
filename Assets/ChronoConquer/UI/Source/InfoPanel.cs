using DevRowInteractive.ChronoConquer.Source.Core;
using DevRowInteractive.ChronoConquer.Source.Core.Macros;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.EntityManagement;
using DevRowInteractive.SelectionManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public class InfoPanel : MonoBehaviour
    {
        private TextMeshProUGUI resourceText;
        private TextMeshProUGUI hitPointsText;
        private TextMeshProUGUI name;
        private Image icon;
        private Resource currentResource;
        private PlayerObject currentPlayerObject;

        private void Awake()
        {
            transform.GetChild(0).GetChild(3).TryGetComponent(out name);
            transform.GetChild(0).GetChild(2).TryGetComponent(out icon);
            transform.GetChild(1).GetChild(0).TryGetComponent(out resourceText);
            transform.GetChild(1).GetChild(1).TryGetComponent(out hitPointsText);

            EventManager.OnSelectableSelected += SetPanelValues;
        }

        private void SetPanelValues(ISelectable selectable)
        {
            var selectableObject = selectable.GetGameObjectReference();

            currentResource = null;
            currentPlayerObject = null;

            if (selectableObject.TryGetComponent<WorldObject>(out var worldObject))
            {
                name.text = worldObject.Name;
                icon.sprite = worldObject.Icon;
            }

            if (selectableObject.TryGetComponent<Resource>(out var resource))
                currentResource = resource;

            if (selectableObject.TryGetComponent<PlayerObject>(out var playerObject))
                currentPlayerObject = playerObject;
        }

        private void LateUpdate()
        {
            if (currentResource)
            {
                resourceText.text = currentResource.CurrentResourceAmount + "/" +
                                    MACROS_RESOURCES.INITIAL_RESOURCE_CAPACITY;
                hitPointsText.text = "";
            }


            if (currentPlayerObject)
            {
                hitPointsText.text = currentPlayerObject.CurrentHitPoints + "/" + currentPlayerObject.InitialHitpoints;

                if (currentPlayerObject.TryGetComponent<Villager>(out var villager))
                {
                    var resource = villager.GetCurrentResource();
                    
                    if(resource != null)
                        resourceText.text = resource.ResourceType + ": " + resource.Amount;
                }
                else
                {
                    resourceText.text = "";
                }
            }
        }
    }
}