public class CharacterDamageComponent : IDamageComponent
{
    public float Damage => 10;

    public void DoDamage(Character target)
    {
        if (target.HealthComponent != null)
            target.HealthComponent.TakeDamage(Damage);
    }
}
