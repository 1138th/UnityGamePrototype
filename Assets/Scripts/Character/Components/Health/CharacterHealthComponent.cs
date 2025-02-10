using System;
using UnityEngine;

public class CharacterHealthComponent : CharacterComponent, IHealthComponent
{
    private float currentHealth;
    private float maxHealth;
    private float hpRegen;
    private float hpRegenDeltaTime;
    private bool isImmortal = false;

    public event Action<Character> OnDeath;

    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = value;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            if (!(currentHealth <= 0)) return;
            currentHealth = 0;
            ExecuteDeath();
        }
    }

    public new void Initialize(Character character)
    {
        base.Initialize(character);

        hpRegenDeltaTime = 1; //1 Second
        maxHealth = character.Data.MaxHealth;
        currentHealth = maxHealth;
    }

    public void RegenerateHealth()
    {
        Character.Data.HealthBar.value = currentHealth;

        if (currentHealth < maxHealth && hpRegen > 0)
        {
            hpRegenDeltaTime -= Time.deltaTime;
            if (hpRegenDeltaTime <= 0)
            {
                currentHealth += hpRegen;
                hpRegenDeltaTime = 1;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isImmortal)
        {
            CurrentHealth -= damage;
            if (Character.Data.HealthBar) Character.Data.HealthBar.value = CurrentHealth;
        }
    }

    public void IncreaseHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;

        Character.Data.HealthBar.maxValue = maxHealth;
        Character.Data.HealthBar.value = currentHealth;
    }

    public void IncreaseHpRegen(float amount)
    {
        hpRegen += amount;
    }

    public void MakeImmortal(bool immortal)
    {
        isImmortal = immortal;
    }

    public void ExecuteDeath()
    {
        OnDeath?.Invoke(Character);
    }
}
