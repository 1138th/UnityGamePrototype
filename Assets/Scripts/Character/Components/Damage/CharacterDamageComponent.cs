public class CharacterDamageComponent : CharacterComponent, IDamageComponent
{
    public void DealDamage(Character target)
    {
        target.HealthComponent?.TakeDamage(MetaManager.Instance.WeaponData.Damage * UpgradesSystem.Instance.DamageAmp);
    }

    public void DealDamageToPlayer(Character target)
    {
        target.HealthComponent?.TakeDamage(Character.Data.Damage * UpgradesSystem.Instance.DamageReductionAmp);
    }
}
