using System;
using UnityEngine;
using UnityEngine.AI;

namespace DevRowInteractive.EntityManagement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ExampleUnit : MonoBehaviour, IDamageable, IAttacking, IMovable
    {
        private NavMeshAgent navMeshAgent;
        private float health = 10;
        private readonly float damage = 10;
        private void Awake() => TryGetComponent(out navMeshAgent);

        public void TakeDamage(float amount)
        {
            if(!IsDead()) health -= amount;
        }

        public void DealDamage(IDamageable damageable) => damageable.TakeDamage(damage);
        public void MakeMovement(Vector3 destination) => navMeshAgent.SetDestination(destination);
        public void StopMovement() => navMeshAgent.isStopped = true;

        public bool IsAtDestination()
        {
            if (!navMeshAgent.pathPending)
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                        return true;

            return false;
        }

        public bool IsDead() => health <= 0;
    }
}