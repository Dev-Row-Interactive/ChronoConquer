using System.Collections;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using DevRowInteractive.EntityManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World
{
    public class Villager : Unit, IGathering<Resource>
    {
        private Resource currentResource;
        private Vector3 currentGatherSpot;

        public void Gather(Resource resource)
        {
            currentResource = resource;
            StartCoroutine(GoToResource());
        }

        private IEnumerator GoToResource()
        {
            currentGatherSpot = currentResource.GetGatherSpot();
            
            MakeMovement(currentGatherSpot);

            while (!IsAtDestination())
                yield return null;

            StartCoroutine(GatherResource());

        }

        private IEnumerator GatherResource()
        {
            yield return new WaitForSeconds(GameManager.Instance.PlayerStatsHandler.GatherTime);

            StartCoroutine(GoToDropDeposit());
        }

        private IEnumerator GoToDropDeposit()
        {
            var closestDropOff = GameManager.Instance.BuildingHandler.GetNearestResourceDropOffPoint(transform.position);
            MakeMovement(closestDropOff.GetDepositPosition());
            currentResource.FreeGatherSpot(currentGatherSpot);
            
            while (!IsAtDestination())
                yield return null;

            StartCoroutine(GoToResource());
        }
    }
}