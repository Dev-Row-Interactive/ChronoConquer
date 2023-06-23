using System.Collections.Generic;
using DevRowInteractive.ChronoConquer.Source.Core.Macros;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World
{
    public class Resource : GaiaObject
    {
        public int CurrentResourceAmount;
        public EResourceType ResourceType;
        private List<Vector3> availableGatherSpots;

        public override void Awake()
        {
            CurrentResourceAmount = MACROS_RESOURCES.INITIAL_RESOURCE_CAPACITY;
            base.Awake();
            EventManager.OnLateInitializeGame += CheckSpots;
        }

        private void SetSpots()
        {
            var trans = transform;
            var right = trans.right;
            var forward = trans.forward;
            var position = trans.position;

            float distanceToResource = 0.66f;
            float distanceBetweenSpots = 0.33f;
            availableGatherSpots = new List<Vector3>()
            {
                // Top
                position + forward * distanceToResource - right * distanceBetweenSpots,
                position + forward * distanceToResource + right * distanceBetweenSpots,
                position + forward * distanceToResource,

                // Right
                position + right * distanceToResource - forward * distanceBetweenSpots,
                position + right * distanceToResource + forward * distanceBetweenSpots,
                position + right * distanceToResource,

                // Bottom
                position - forward * distanceToResource - right * distanceBetweenSpots,
                position - forward * distanceToResource + right * distanceBetweenSpots,
                position - forward * distanceToResource,

                // Left
                position - right * distanceToResource - forward * distanceBetweenSpots,
                position - right * distanceToResource + forward * distanceBetweenSpots,
                position - right * distanceToResource,
            };
        }

        private void OnDrawGizmos()
        {
            if (!MACROS_RESOURCES.SHOW_RESOURCE_GATHER_SPOTS)
                return;

            if (availableGatherSpots == null) return;

            Gizmos.color = Color.red;
            foreach (Vector3 spot in availableGatherSpots)
            {
                Gizmos.DrawSphere(spot, 0.1f); // Draws a red sphere with a radius of 0.1
            }
        }

        public void SetGatherSpots(List<Vector3> spots) => availableGatherSpots = spots;

        public List<Vector3> GetGatherSpots() => availableGatherSpots;

        public bool CanBeGathered() => availableGatherSpots.Count > 0;

        public Vector3 GetNearestGatherSpot(Vector3 target)
        {
            var nextSpot = Helpers.HelperMaths.FindNearest(target, availableGatherSpots);
            availableGatherSpots.Remove(nextSpot);
            return nextSpot;
        }

        public override void Register()
        {
            base.Register();
            SetSpots();
            GameManager.Instance.Gaia.RegisterResource(this);
            GameManager.Instance.Map.SetTileOccupied(transform.position, gameObject);
        }

        private void CheckSpots()
        {
            var trans = transform;
            var right = trans.right;
            var forward = trans.forward;
            var position = trans.position;
            
            // Create a new list to hold spots that will be removed
            List<Vector3> spotsToRemove = new List<Vector3>();
            
            if (GameManager.Instance.Map.GetTileReferenceAtPosition(position + forward))
            {
                spotsToRemove.Add(availableGatherSpots[0]);
                spotsToRemove.Add(availableGatherSpots[1]);
                spotsToRemove.Add(availableGatherSpots[2]);
            }
            if (GameManager.Instance.Map.GetTileReferenceAtPosition(position + right))
            {
                spotsToRemove.Add(availableGatherSpots[3]);
                spotsToRemove.Add(availableGatherSpots[4]);
                spotsToRemove.Add(availableGatherSpots[5]);
            }
            if (GameManager.Instance.Map.GetTileReferenceAtPosition(position - forward))
            {
                spotsToRemove.Add(availableGatherSpots[6]);
                spotsToRemove.Add(availableGatherSpots[7]);
                spotsToRemove.Add(availableGatherSpots[8]);
            }
            if (GameManager.Instance.Map.GetTileReferenceAtPosition(position - right))
            {
                spotsToRemove.Add(availableGatherSpots[9]);
                spotsToRemove.Add(availableGatherSpots[10]);
                spotsToRemove.Add(availableGatherSpots[11]);
            }

            // Remove all the spots in spotsToRemove from availableGatherSpots
            foreach (var spot in spotsToRemove)
            {
                availableGatherSpots.Remove(spot);
            }
        }

        public void FreeGatherSpot(Vector3 spot) => availableGatherSpots.Add(spot);
    }
}