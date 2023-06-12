using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.EntityManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.Handlers
{
    public class BuildingHandler
    {
        private List<Building> buildings = new List<Building>();

        private List<IDepositable<EResourceType, int>> resourceDropOffPoint =
            new List<IDepositable<EResourceType, int>>();
        public void RegisterBuilding(Building building)
        {
            buildings.Add(building);

            if (building.TryGetComponent<IDepositable<EResourceType, int>>(out var depositable))
            {
                resourceDropOffPoint.Add(depositable);
            }
        }
        
        public IDepositable<EResourceType, int> GetNearestResourceDropOffPoint(Vector3 position)
        {
            IDepositable<EResourceType, int> nearestDropOffPoint = null;
            float nearestDistance = float.MaxValue;

            foreach (var depositable in resourceDropOffPoint)
            {
                Vector3 dropOffPointPosition = depositable.GetDepositPosition();
                float distance = Vector3.Distance(position, dropOffPointPosition);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestDropOffPoint = depositable;
                }
            }

            return nearestDropOffPoint;
        }

    }
}