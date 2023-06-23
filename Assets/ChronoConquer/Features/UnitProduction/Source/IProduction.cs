using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevRowInteractive.UnitProduction
{
    public interface IProduction
    {
        public IEnumerator StartProduction();
        public float GetProductionProgress();
        public void StopProduction();
        public void FinishProduction();
        public void AddToQueue(IProduceable produceable);
        public void RemoveFromQueue();
        public List<GameObject> GetProduceables();
        public int GetQueueCount();
    }
}