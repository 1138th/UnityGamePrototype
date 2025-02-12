public interface IDamageComponent : ICharacterComponent
{
    public void DealDamage(Character target);

    public void DealDamage(Character target, float damage);

    public void DealDamageToPlayer(Character target);
}
