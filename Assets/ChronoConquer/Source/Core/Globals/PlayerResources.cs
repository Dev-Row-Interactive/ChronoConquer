using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.World;

namespace DevRowInteractive.ChronoConquer.Source.Core.Globals
{
    public class PlayerResources
    {
        public List<ResourceCount> Resources = new List<ResourceCount>()
        {
            new ResourceCount(EResourceType.Wood, 200),
            new ResourceCount(EResourceType.Food, 200),
            new ResourceCount(EResourceType.Gold, 100),
            new ResourceCount(EResourceType.Stone, 100)
        };

        public void ModifyResourceAmount(ResourceCount res)
        {
            foreach (var resource in Resources)
            {
                if (resource.ResourceType == res.ResourceType)
                {
                    resource.Amount += res.Amount;
                    EventManager.InvokeResourceAmountChanged(resource);
                }
            }
        }

        public int GetResourceAmount(EResourceType resourceType)
        {
            foreach (var resource in Resources)
            {
                if (resource.ResourceType == resourceType)
                    return resource.Amount;
            }

            return 0; // Resource not found, return 0 as default amount
        }
    }
}