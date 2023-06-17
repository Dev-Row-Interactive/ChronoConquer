using DevRowInteractive.EntityManagement;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(RVOController))]
    public abstract class Unit : PlayerObject, IMovable
    {
        public AIPath Agent;
        public int Armor;
        public int Speed;
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
    }
}