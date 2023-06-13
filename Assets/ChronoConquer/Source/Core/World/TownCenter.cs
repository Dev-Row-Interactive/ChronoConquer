using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.EntityManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World
{
    public class TownCenter : Building , IDepositable
    {
        public Collider GetCollider()
        {
            Collider collider = GetComponent<Collider>();

            if (collider == null)
            {
                Debug.LogWarning("No collider attached to IDepositable");
            }
                
            return collider;
        }
    }
}