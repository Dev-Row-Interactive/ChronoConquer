namespace DevRowInteractive.EntityManagement
{
    public interface IDamageable
    {
        void TakeDamage(float amount);
        bool IsDead();
    }
}