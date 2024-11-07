using System;
using UnityEngine;

public class ImmortalComponent : CharacterComponent, IHealthComponent
{
    public float MaxHealth { get => Character.Data.MaxHealth; private set { } }
    public event Action<Character> OnDeath;
    public float CurrentHealth { get; private set; }

    public void TakeDamage(float damage)
    {
        Debug.Log("Character is immortal.");
    }
}
