using DevRowInteractive.ChronoConquer.Source.Core;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using TMPro;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public class ResourceField : MonoBehaviour
    {
        [SerializeField] private EResourceType resourceType;
        private TextMeshProUGUI countText;
        
        private void Awake()
        {
            transform.GetChild(0).TryGetComponent(out countText);
            EventManager.OnGameInitialize += Initialize;
        }

        // Initialize Values
        private void Initialize()
        {
            EventManager.OnResourceAmountChanged += UpdateResources;
            UpdateResources();
        }

        private void UpdateResources() => countText.text = GameManager.Instance.PlayerResources.GetResourceAmount(resourceType).ToString();
    }
}
