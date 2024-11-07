using System;

public interface IHealthComponent : ICharacterComponent
{
    public event Action<Character> OnDeath; 
    public float CurrentHealth { get; }

    public void TakeDamage(float damage);
}
