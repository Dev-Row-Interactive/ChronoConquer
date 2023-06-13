using System.Collections.Generic;
using System.Linq;
using DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World
{
    public class Resource : GaiaObject
    {
        public EResourceType ResourceType;
        private List<Vector3> availableGatherSpots;

        public override void Start()
        {
            base.Start();
            availableGatherSpots = new List<Vector3>()
            {
                transform.position + transform.forward,
                transform.position - transform.forward,
                transform.position + transform.right,
                transform.position - transform.right
            };
        }

        public void SetGatherSpots(List<Vector3> spots) => availableGatherSpots = spots;

        public List<Vector3> GetGatherSpots() => availableGatherSpots;

        public bool CanBeGathered() => availableGatherSpots.Count > 0;

        public Vector3 GetGatherSpot()
        {
            Vector3 spot = availableGatherSpots.Last();
            availableGatherSpots.Remove(spot);
            return spot;
        }

        public override void Register()
        {
            base.Register();
            GameManager.Instance.Gaia.RegisterResource(this);
        }

        public void FreeGatherSpot(Vector3 spot)
        {
            availableGatherSpots.Add(spot);
        }
    }
}