public interface IDamageComponent : ICharacterComponent
{
    public void DealDamage(Character target);

    public void DealDamageToPlayer(Character target);
}
