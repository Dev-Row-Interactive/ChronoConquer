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
        public virtual void Awake() => TryGetComponent(out Agent);

        public override void Start()
        {
            base.Start();
            Agent.maxSpeed = Speed;
        }

        public override bool IsMultiSelect() => true;
        
        public void MakeMovement(Vector3 destination) => Agent.destination = destination;
        public void StopMovement() => Agent.isStopped = true;
        public bool IsAtDestination() => Agent.reachedDestination;
    }
}