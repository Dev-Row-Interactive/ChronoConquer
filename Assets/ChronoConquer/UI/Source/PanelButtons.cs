using System.Collections.Generic;
using DevRowInteractive.UnitProduction;
using DevRowInteractive.ChronoConquer.Source.Core;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.SelectionManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public class PanelButtons : MonoBehaviour
    {
        [SerializeField] private GameObject PanelButtonPrefab;

        private List<GameObject> buttons = new List<GameObject>();

        private void Awake()
        {
            EventManager.OnSelectableSelected += SetButtons;
            EventManager.OnSelectableDeSelected += ResetButtons;
        }

        private void SetButtons(ISelectable selectable)
        {
            var selectedObject = selectable.GetGameObjectReference();

            if (selectedObject.TryGetComponent<IProduction>(out var production))
            {
                foreach (var produceableObject in production.GetProduceables())
                {
                    produceableObject.TryGetComponent<WorldObject>(out var worldObject);
                    produceableObject.TryGetComponent<IProduceable>(out var produceable);

                    var buttonInstance = Instantiate(PanelButtonPrefab, transform);
                    var panelButton = buttonInstance.GetComponent<PanelButton>();

                    panelButton.SetUpButton(worldObject.Icon, production);
                    panelButton.GetButton().onClick.AddListener(() => UpdateQueue(production, produceable));

                    buttons.Add(buttonInstance);
                }
            }
        }

        private void UpdateQueue(IProduction production, IProduceable produceable) =>
            production.AddToQueue(produceable);

        private void ResetButtons(ISelectable selectable)
        {
            if (buttons.Count <= 0)
                return;

            foreach (var button in buttons)
                Destroy(button);
        }
    }
}