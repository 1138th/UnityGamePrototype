public interface IPlayerLogicComponent : ILogicComponent
{
    public void PlayerPcMove(Character target);

    public void PlayerMobileMove(Character target);

    public void PlayerDash();
}