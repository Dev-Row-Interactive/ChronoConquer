using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core
{
    public class PlayerStatsHandler
    {
        public List<GameObject> SelectableObjects = new List<GameObject>();
        public List<ResourceCount> Resources = new List<ResourceCount>()
        {
            new ResourceCount(EResourceType.Wood, 0),
            new ResourceCount(EResourceType.Food, 0),
            new ResourceCount(EResourceType.Gold, 0),
            new ResourceCount(EResourceType.Stone, 0)
        };

        public float GatherTime = 5f;
    }
}