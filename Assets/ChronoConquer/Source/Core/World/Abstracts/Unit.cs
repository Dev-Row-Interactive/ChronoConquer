using System.Collections.Generic;
using DevRowInteractive.EntityManagement;
using DevRowInteractive.UnitProduction;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(RVOController))]
    public abstract class Unit : PlayerObject, IMovable, IProduceable
    {
        [HideInInspector] public AIPath Agent;
        public int Armor;
        public int Speed;
        public float ProductionTime;
        public List<ResourceCount> Costs;
        public GameObject Prefab;
        public override void Awake()
        {
            base.Awake();
            TryGetComponent(out Agent);
            Agent.maxSpeed = Speed;
        }
        
        public override bool IsMultiSelect() => true;
        
        public void MakeMovement(Vector3 destination) => Agent.destination = destination;
        public void StopMovement() => Agent.isStopped = true;
        public bool IsAtDestination() => Agent.reachedDestination;

        public GameObject GetGameObjectReference()
        {
            return gameObject;
        }
    }
}