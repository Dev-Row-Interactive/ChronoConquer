using UnityEngine;

namespace DevRowInteractive.EntityManagement
{
    public interface IMovable
    {
        void MakeMovement(Vector3 destination);
        void StopMovement();
        bool IsAtDestination();
    }
}