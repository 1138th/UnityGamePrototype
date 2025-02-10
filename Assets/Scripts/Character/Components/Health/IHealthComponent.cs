using System;

public interface IHealthComponent : ICharacterComponent
{
    public event Action<Character> OnDeath; 
    public float CurrentHealth { get; }

    public void TakeDamage(float damage);

    public void IncreaseHealth(float amount);

    public void RegenerateHealth();

    public void IncreaseHpRegen(float amount);

    public void MakeImmortal(bool isImmortal);
}
