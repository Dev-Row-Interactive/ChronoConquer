namespace DevRowInteractive.EntityManagement
{
    /// <summary>
    /// This interface uses a generic to decouple the feature from the framework.
    /// The generic can be overridden with a type when implemented.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGathering <in T>
    {
        void Gather(T resource);
    }
}