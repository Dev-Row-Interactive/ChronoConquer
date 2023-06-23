using System.Collections;
using System.Collections.Generic;
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
        private bool breakRoutine;

        public void Gather(Resource resource)
        {
            if (gatherCoroutine != null)
                Reset();

            currentResource = resource;
            gatherCoroutine = StartCoroutine(GoToResource());
        }

        public void Deliver(IDepositable target)
        {
            gatherCoroutine = StartCoroutine(GoToDropDeposit(target));
            breakRoutine = true;
        }

        public ResourceCount GetCurrentResource()
        {
            return resourceCount;
        }

        private IEnumerator GoToResource()
        {
            List<Resource> exceptions = new List<Resource>();
            // Start a loop to continuously search for a resource
            while (!currentResource.CanBeGathered())
            {
                exceptions.Add(currentResource);
                // Search for the nearest resource of the right type
                currentResource = GameManager.Instance.Gaia.GetNearestResourceOfType(currentResource.transform.position,
                    currentResource.ResourceType, exceptions);
                
                if (currentResource == null)
                {
                    if (gatherCoroutine != null)
                    {
                        // If no resource of the right type is found, stop the coroutine
                        StopCoroutine(gatherCoroutine);
                    }
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
                if(breakRoutine)
                    yield break;
                
                completeCycleTime += Time.deltaTime;
                individualResourceTime += Time.deltaTime;

                if (individualResourceTime >= gatherRatePerSecond)
                {
                    currentResource.CurrentResourceAmount--;
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

        private IEnumerator GoToDropDeposit(IDepositable target = null)
        {
            if(currentResource)
                currentResource.FreeGatherSpot(currentGatherSpot);
            
            var closestDropOff = GameManager.Instance.BuildingHandler.FindNearestBoundBorderPosition(transform, target);
            MakeMovement(closestDropOff);

            while (!IsAtDestination())
                yield return null;

            breakRoutine = false;

            GameManager.Instance.PlayerResources.ModifyResourceAmount(resourceCount);
            resourceCount = null;

            if (target == null)
                gatherCoroutine = StartCoroutine(GoToResource());

            else
                Reset();
        }
        
    }
}