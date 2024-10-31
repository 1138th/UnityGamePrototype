public class CharacterDamageComponent : CharacterComponent, IDamageComponent
{
    public void DoDamage(Character target)
    {
        if (target.HealthComponent != null)
            target.HealthComponent.TakeDamage(Character.characterData.Damage);
    }
}
