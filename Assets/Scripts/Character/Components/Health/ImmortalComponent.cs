using UnityEngine;

public class ImmortalComponent : CharacterComponent, IHealthComponent
{
    public float MaxHealth { get => Character.data.MaxHealth; private set { } }
    public float CurrentHealth { get; private set; }

    public void TakeDamage(float damage)
    {
        Debug.Log("Character is immortal.");
    }
}
