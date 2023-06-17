using System.Collections.Generic;
using System.Linq;
using DevRowInteractive.ChronoConquer.Source.Core.World;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.Globals
{
    public class Gaia
    {
        private List<Resource> resources = new List<Resource>();

        public Resource GetNearestResource(Vector3 position, Resource exception = null)
        {
            Resource nearestResource = null;
            float shortestDistance = float.MaxValue;

            foreach (Resource resource in resources)
            {
                if (resource == exception)
                    continue;

                float distance = Vector3.Distance(position, resource.gameObject.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestResource = resource;
                }
            }

            return nearestResource;
        }

        public Resource GetNearestResourceOfType(Vector3 position, EResourceType type, Resource exception = null)
        {
            Resource nearestResource = null;
            float shortestDistance = float.MaxValue;

            foreach (Resource resource in resources)
            {
                if (resource == exception)
                    continue;

                if (resource.ResourceType != type)
                    continue;

                float distance = Vector3.Distance(position, resource.gameObject.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestResource = resource;
                }
            }

            return nearestResource;
        }

        public void RegisterResource(Resource resource)
        {
            resources.Add(resource);
            SetResourceGatherSpots();
        }

        private void SetResourceGatherSpots()
        {
            foreach (var resource in resources)
            {
                var gatherSpots = resource.GetGatherSpots();

                // Check if any other resource is occupying the gather spot
                foreach (var spot in gatherSpots.ToList()) // Create a copy of the list to iterate over
                {
                    var occupyingResource = GetResourceAtPosition(spot, resource);
                    if (occupyingResource != null)
                    {
                        gatherSpots.Remove(spot);
                    }
                }

                resource.SetGatherSpots(gatherSpots);
            }
        }

        private Resource GetResourceAtPosition(Vector3 position, Resource exception = null)
        {
            foreach (var resource in resources)
            {
                if (resource == exception)
                    continue;

                if (Vector3.Distance(position, resource.transform.position) < 0.1f)
                    return resource;
            }

            return null;
        }
    }
}