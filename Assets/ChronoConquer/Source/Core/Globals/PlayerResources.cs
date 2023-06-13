using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.World;

namespace DevRowInteractive.ChronoConquer.Source.Core.Globals
{
    public class PlayerResources
    {
        public List<ResourceCount> Resources = new List<ResourceCount>()
        {
            new ResourceCount(EResourceType.Wood, 0),
            new ResourceCount(EResourceType.Food, 0),
            new ResourceCount(EResourceType.Gold, 0),
            new ResourceCount(EResourceType.Stone, 0)
        };

        public void ModifyResourceAmount(ResourceCount res)
        {
            foreach (var resource in Resources)
            {
                if (resource.ResourceType == res.ResourceType)
                    resource.Amount += res.Amount;
            }
        }
    }
}