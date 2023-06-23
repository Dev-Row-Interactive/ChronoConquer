using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevRowInteractive.UnitProduction;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts
{
    public abstract class ProductionBuilding : PlayerObject, IProduction
    {
        private Coroutine productionRoutine;
        protected Queue<IProduceable> ProductionQueue = new Queue<IProduceable>();
        public List<GameObject> Produceables;
        public Transform RallyPoint;

        private float productionProgress;

        private Resource currentRallyPointResource;

        public override void Awake()
        {
            base.Awake();
            RallyPoint = transform.GetChild(0).transform.GetChild(0);
            SetRallyPoint(RallyPoint.position);
            RallyPoint.gameObject.SetActive(false);
        }

        public override void Select()
        {
            base.Select();
            RallyPoint.gameObject.SetActive(true);
        }

        public override void DeSelect()
        {
            base.DeSelect();
            RallyPoint.gameObject.SetActive(false);
        }
        
        public void SetRallyPoint(Vector3 position, Resource resource = null)
        {
            currentRallyPointResource = resource;

            RallyPoint.position = position;

            LineRenderer lineRenderer;

            if (!RallyPoint.gameObject.GetComponent<LineRenderer>())
                lineRenderer = RallyPoint.gameObject.AddComponent<LineRenderer>();

            else
                lineRenderer = RallyPoint.GetComponent<LineRenderer>();

            lineRenderer.startWidth = 0.04f;
            lineRenderer.endWidth = 0.04f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.alignment = LineAlignment.View;


            // Draw line to the Rally Point
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, RallyPoint.position);
        }

        public override void Register()
        {
            base.Register();
            GameManager.Instance.BuildingHandler.RegisterBuilding(this);
        }

        public IEnumerator StartProduction()
        {
            var produceable = ProductionQueue.First();
            produceable.GetGameObjectReference().TryGetComponent<Unit>(out var unit);

            float t = 0;

            while (t < unit.ProductionTime)
            {
                t += Time.deltaTime;
                productionProgress = t / unit.ProductionTime;
                yield return null;
            }

            var instance = Instantiate(unit.Prefab, transform.position - (transform.right * 3), Quaternion.identity);
            var unitInstance = instance.GetComponent<Unit>();
            unitInstance.Register();

            if (currentRallyPointResource)
            {
                if (unitInstance.GetGameObjectReference().TryGetComponent<Villager>(out var villager))
                    villager.Gather(currentRallyPointResource);
            }
            else
            {
                unitInstance.MakeMovement(RallyPoint.position);
            }

            FinishProduction();
        }

        public void FinishProduction()
        {
            RemoveFromQueue();
            if (GetQueueCount() > 0)
                productionRoutine = StartCoroutine(StartProduction());
            else
                productionRoutine = null;
        }

        public void AddToQueue(IProduceable produceable)
        {
            produceable.GetGameObjectReference().TryGetComponent<Unit>(out var unit);

            foreach (var resource in unit.Costs)
            {
                ResourceCount resourceCount = new ResourceCount(resource.ResourceType, -resource.Amount);

                if (GameManager.Instance.PlayerResources.GetResourceAmount(resourceCount.ResourceType) <
                    resource.Amount)
                    return;

                GameManager.Instance.PlayerResources.ModifyResourceAmount(resourceCount);
            }

            ProductionQueue.Enqueue(produceable);

            if (productionRoutine == null)
                productionRoutine = StartCoroutine(StartProduction());
        }

        public override bool IsMultiSelect() => false;
        public float GetProductionProgress() => productionProgress;
        public void StopProduction() => RemoveFromQueue();
        public void RemoveFromQueue() => ProductionQueue.Dequeue();
        public List<GameObject> GetProduceables() => Produceables;
        public int GetQueueCount() => ProductionQueue.Count;
    }
}