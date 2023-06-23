using DevRowInteractive.UnitProduction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public class PanelButton : MonoBehaviour
    {
        private Image icon;
        private Button button;
        private Slider progressBar;
        private TextMeshProUGUI queueCounter;
        private IProduction production;

        private void Awake()
        {
            progressBar = transform.GetChild(3).GetComponent<Slider>();
            icon = transform.GetChild(1).GetComponent<Image>();
            button = GetComponent<Button>();
            queueCounter = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (production != null)
            {
                queueCounter.gameObject.SetActive(production.GetQueueCount() > 0);
                progressBar.gameObject.SetActive(production.GetQueueCount() > 0);
                
                progressBar.value = production.GetProductionProgress(); 
                queueCounter.text = production.GetQueueCount().ToString();
            }
        }

        public void SetUpButton(Sprite sprite, IProduction building)
        {
            icon.sprite = sprite;
            production = building;
        }

        public Button GetButton() => button;
    }
}