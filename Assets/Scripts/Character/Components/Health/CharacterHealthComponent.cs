using UnityEngine;

public class CharacterHealthComponent : CharacterComponent, IHealthComponent
{
    private float currentHealth;

    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = value;
            if (currentHealth > Character.data.MaxHealth)
                currentHealth = Character.data.MaxHealth;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                DoDeath();
            }
        }
    }

    public new void Initialize(Character character)
    {
        base.Initialize(character);
        CurrentHealth = Character.data.MaxHealth;
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
