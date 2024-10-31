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
            if (currentHealth > Character.characterData.MaxHealth)
                currentHealth = Character.characterData.MaxHealth;
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
        CurrentHealth = Character.characterData.MaxHealth;
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
