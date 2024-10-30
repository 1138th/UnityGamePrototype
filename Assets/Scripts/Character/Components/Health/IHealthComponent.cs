public interface IHealthComponent
{
    public float MaxHealth { get; }
    public float CurrentHealth { get; }

    public void TakeDamage(float damage);
}
