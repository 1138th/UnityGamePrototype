using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalComponent : IHealthComponent
{
    public float MaxHealth { get => 1; set { } }
    public float CurrentHealth { get => 1; set { } }

    public void TakeDamage(float damage)
    {
        Debug.Log("Character is immortal.");
    }
}
