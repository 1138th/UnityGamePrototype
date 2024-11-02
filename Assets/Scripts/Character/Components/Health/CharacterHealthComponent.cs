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
        Character.Data.HealthBar.maxValue = Character.Data.MaxHealth;
        Character.Data.HealthBar.value = Character.Data.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Character.Data.HealthBar.value = CurrentHealth;
    }

    public void ExecuteDeath()
    {
        Debug.Log("Character died.");
    }
}
