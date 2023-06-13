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
            // Search for another resource recursively
            while (!currentResource.CanBeGathered())
            {
                currentResource = GameManager.Instance.Gaia.GetNearestResource(transform.position, currentResource);
                if (currentResource == null)
                    yield break; // Exit the coroutine if no resource is found
                yield return null;
            }
            
            currentGatherSpot = currentResource.GetGatherSpot();

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