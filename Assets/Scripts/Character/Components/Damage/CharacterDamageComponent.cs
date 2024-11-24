public class CharacterDamageComponent : CharacterComponent, IDamageComponent
{
    public void DealDamage(Character target)
    {
        if (target.HealthComponent != null)
            target.HealthComponent.TakeDamage(MetaManager.Instance.WeaponData.Damage);
    }

    public void DealDamageToPlayer(Character target)
    {
        if (target.HealthComponent != null)
            target.HealthComponent.TakeDamage(Character.Data.Damage);
    }
}
