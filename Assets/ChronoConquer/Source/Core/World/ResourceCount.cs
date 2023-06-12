using System;

namespace DevRowInteractive.ChronoConquer.Source.Core.World
{
    [Serializable]
    public class ResourceCount
    {
        public EResourceType ResourceType;
        public int Amount;

        public ResourceCount(EResourceType resourceType, int amount)
        {
            this.ResourceType = resourceType;
            this.Amount = amount;
        }
        
        public void ChangeAmount(int amount) => Amount += amount;
    }
}