using System;
using UnityEngine;

public class CharacterHealthComponent : CharacterComponent, IHealthComponent
{
    private float currentHealth;

    public event Action<Character> OnDeath; 
    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = value;
            if (currentHealth > Character.Data.MaxHealth) currentHealth = Character.Data.MaxHealth;
            if (!(currentHealth <= 0)) return;
            currentHealth = 0;
            ExecuteDeath();
        }
    }

    public new void Initialize(Character character)
    {
        base.Initialize(character);
        
        CurrentHealth = character.Data.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (Character.Data.HealthBar) Character.Data.HealthBar.value = CurrentHealth;
    }

    public void ExecuteDeath()
    {
        OnDeath?.Invoke(Character);
    }
}
