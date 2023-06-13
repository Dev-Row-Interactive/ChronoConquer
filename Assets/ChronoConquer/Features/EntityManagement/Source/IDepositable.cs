using UnityEngine;

namespace DevRowInteractive.EntityManagement
{
    /// <summary>
    /// This interface uses two generics to decouple the feature from the framework.
    /// The generics can be overridden with types when implemented.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IDepositable
    {
        Collider GetCollider();
    }
}