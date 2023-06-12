using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.EntityManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World
{
    public class TownCenter : Building , IDepositable<EResourceType, int>
    {
        public Vector3 GetDepositPosition() => transform.position - (transform.forward * 2);
        
        public void Deposit(EResourceType resourceType, int amount)
        {
            foreach (var resource in GameManager.Instance.PlayerStatsHandler.Resources)
            {
                if(resource.ResourceType == resourceType)
                    resource.ChangeAmount(amount);
            }
        }
    }
}