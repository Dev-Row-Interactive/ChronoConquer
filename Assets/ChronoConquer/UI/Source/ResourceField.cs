using DevRowInteractive.ChronoConquer.Source.Core;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public class ResourceField : MonoBehaviour
    {
        [SerializeField] private EResourceType resource;
        [SerializeField] private Sprite sprite;
        private TextMeshProUGUI countText;
        private Image thumbnail;
        private int currentCount = 0;
        
        private void Awake()
        {
            transform.GetChild(0).TryGetComponent(out countText);
            transform.GetChild(2).TryGetComponent(out thumbnail);
            EventManager.OnLateGameInitialize += GameInitialize;
        }

        // Initialize Values
        private void GameInitialize()
        {
            EventManager.OnResourceAmountChanged += UpdateResources;
            thumbnail.sprite = sprite;

            ResourceCount resourceCount = new ResourceCount(resource,
                GameManager.Instance.PlayerResources.GetResourceAmount(resource));
            
            UpdateResources(resourceCount);
        }

        private void UpdateResources(ResourceCount resourceCount)
        {
            if(resource == resourceCount.ResourceType)
                countText.text = (currentCount + resourceCount.Amount).ToString();
        }
    }
}
