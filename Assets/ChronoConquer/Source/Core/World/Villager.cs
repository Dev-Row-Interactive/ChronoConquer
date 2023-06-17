using System.Collections;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.EntityManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World
{
    public class Villager : Unit, IGathering<Resource>
    {
        private ResourceCount resourceCount;
        private Resource currentResource;
        private Vector3 currentGatherSpot;
        private Coroutine gatherCoroutine;

        public void Gather(Resource resource)
        {
            if (gatherCoroutine != null)
                Reset();
            
            currentResource = resource;
            gatherCoroutine = StartCoroutine(GoToResource());
        }

        private IEnumerator GoToResource()
        {
            // Start a loop to continuously search for a resource
            while (!currentResource.CanBeGathered() && currentGatherSpot == Vector3.zero)
            {
                // Search for the nearest resource of the right type
                currentResource = GameManager.Instance.Gaia.GetNearestResourceOfType(currentResource.transform.position, currentResource.ResourceType, currentResource);

                if (currentResource == null)
                {
                    // If no resource of the right type is found, stop the coroutine
                    StopCoroutine(gatherCoroutine);
                    Reset();
                    yield break;
                }
            }
            
            currentGatherSpot = currentResource.GetNearestGatherSpot(transform.position);

            MakeMovement(currentGatherSpot);

            while (!IsAtDestination())
                yield return null;

            gatherCoroutine = StartCoroutine(GatherResource());
        }

        private IEnumerator GatherResource()
        {
            if (resourceCount == null || resourceCount.ResourceType != currentResource.ResourceType)
                resourceCount = new ResourceCount(currentResource.ResourceType, 0);

            float completeCycleTime = 0f;
            float individualResourceTime = 0f;
            float gatherRatePerSecond = GameManager.Instance.PlayerStats.ResourceGatherTime /
                                        GameManager.Instance.PlayerStats.MaxVillagerResourceCount;

            while (resourceCount.Amount < GameManager.Instance.PlayerStats.MaxVillagerResourceCount)
            {
                completeCycleTime += Time.deltaTime;
                individualResourceTime += Time.deltaTime;

                if (individualResourceTime >= gatherRatePerSecond)
                {
                    resourceCount.Amount++;
                    individualResourceTime -= gatherRatePerSecond; // Subtract the gather rate from the individual time
                }

                yield return null;
            }

            gatherCoroutine = StartCoroutine(GoToDropDeposit());
        }

        public override void Reset()
        {
            if (currentResource)
            {
                currentResource.FreeGatherSpot(currentGatherSpot);
                currentResource = null;
                currentGatherSpot = Vector3.zero;
            }

            if (gatherCoroutine != null)
                StopCoroutine(gatherCoroutine);
        }

        private IEnumerator GoToDropDeposit()
        {
            var closestDropOff = GameManager.Instance.BuildingHandler.FindNearestBoundBorderPosition(transform);
            MakeMovement(closestDropOff);
            currentResource.FreeGatherSpot(currentGatherSpot);

            while (!IsAtDestination())
                yield return null;

            GameManager.Instance.PlayerResources.ModifyResourceAmount(resourceCount);
            resourceCount = null;

            gatherCoroutine = StartCoroutine(GoToResource());
        }
    }
}