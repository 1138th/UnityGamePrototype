using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthComponent : IHealthComponent
{
    private float currentHealth;

    public float MaxHealth { get => 50; protected set { return; } }
    public float CurrentHealth
    {
        get => currentHealth;
        protected set
        {
            currentHealth = value;
            if (currentHealth > MaxHealth)
                currentHealth = MaxHealth;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                DoDeath();
            }
        }
    }

    public CharacterHealthComponent()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Took damage: " + damage);
    }

    public void DoDeath()
    {
        Debug.Log("Character died.");
    }
}
