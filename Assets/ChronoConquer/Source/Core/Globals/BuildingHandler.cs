﻿using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.EntityManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.Globals
{
    public class BuildingHandler
    {
        private List<Building> buildings = new List<Building>();

        private List<IDepositable> resourceDropOffPoints =
            new List<IDepositable>();
        public void RegisterBuilding(Building building)
        {
            buildings.Add(building);

            if (building.TryGetComponent<IDepositable>(out var depositable))
            {
                resourceDropOffPoints.Add(depositable);
            }
        }

        public Vector3 FindNearestBoundBorderPosition(Transform target)
        {
            IDepositable nearestDepositable = null;
            float nearestDistance = float.MaxValue;
            Vector3 nearestPosition = Vector3.zero;

            foreach (IDepositable depositable in resourceDropOffPoints)
            {
                Collider closestCollider = depositable.GetCollider();

                Vector3 closestPoint = closestCollider.ClosestPoint(target.position);
                float distance = Vector3.Distance(target.position, closestPoint);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestPosition = closestPoint;
                }
            }

            // Offset the nearest position by 1 unit away from the bounds
            Vector3 offsetDirection = (target.position - nearestPosition).normalized;
            nearestPosition += offsetDirection;

            return nearestPosition;
        }

    }
}