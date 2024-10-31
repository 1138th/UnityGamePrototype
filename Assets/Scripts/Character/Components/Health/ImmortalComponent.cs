using UnityEngine;

public class ImmortalComponent : CharacterComponent, IHealthComponent
{
    public float MaxHealth { get => Character.characterData.MaxHealth; set { } }
    public float CurrentHealth { get; set; }

    public void TakeDamage(float damage)
    {
        Debug.Log("Character is immortal.");
    }
}
