using UnityEngine;

namespace DevRowInteractive.EntityManagement
{
    /// <summary>
    /// This interface uses two generics to decouple the feature from the framework.
    /// The generics can be overridden with types when implemented.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IDepositable <in T, in T2>
    {
        Vector3 GetDepositPosition();
        void Deposit(T resourceType, T2 amount);
    }
}