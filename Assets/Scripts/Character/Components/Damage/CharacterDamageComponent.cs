public class CharacterDamageComponent : CharacterComponent, IDamageComponent
{
    public void DealDamage(Character target)
    {
        DealDamage(target, 
            MetaManager.Instance.WeaponData.Damage
            * UpgradesSystem.DamageAmp
            * EventManager.DmgSpawnAmp);
    }

    public void DealDamage(Character target, float damage)
    {
        target.HealthComponent?.TakeDamage(damage);
    }

    public void DealDamageToPlayer(Character target)
    {
        target.HealthComponent?.TakeDamage(Character.Data.Damage * UpgradesSystem.DamageReductionAmp);
    }
}
