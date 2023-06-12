using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Helpers
{
    public static class HelperMaths
    {
        /// <summary>
        /// Returns the Vector3 that's nearest to the target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="vectorList"></param>
        /// <returns></returns>
        public static Vector3 FindNearest(Vector3 target, List<Vector3> vectorList)
        {
            if (vectorList == null || vectorList.Count == 0)
            {
                Debug.LogError("Vector list is null or empty.");
                return Vector3.zero;
            }

            Vector3 nearestVector = vectorList[0];
            float nearestDistance = Vector3.Distance(target, nearestVector);

            for (int i = 1; i < vectorList.Count; i++)
            {
                float distance = Vector3.Distance(target, vectorList[i]);
                if (distance < nearestDistance)
                {
                    nearestVector = vectorList[i];
                    nearestDistance = distance;
                }
            }
            
            return nearestVector;
        }
    }
}

