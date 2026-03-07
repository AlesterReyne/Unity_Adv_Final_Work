namespace Interfaces
{
    public interface IDamageable
    {
        float TakeDamage(float damage);

        bool IsDead(float health);
    }
}