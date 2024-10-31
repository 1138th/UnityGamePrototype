public interface IHealthComponent : ICharacterComponent
{
    public float CurrentHealth { get; }

    public void TakeDamage(float damage);
}
