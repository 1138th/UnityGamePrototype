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
            if (currentHealth > Character.data.MaxHealth) currentHealth = Character.data.MaxHealth;
            if (!(currentHealth <= 0)) return;
            currentHealth = 0;
            ExecuteDeath();
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

    public void ExecuteDeath()
    {
        Debug.Log("Character died.");
    }
}
