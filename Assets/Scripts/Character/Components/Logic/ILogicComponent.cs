public interface ILogicComponent : ICharacterComponent
{
    public void PlayerPcMove(Character target);

    public void PlayerMobileMove(Character target);

    public void PlayerDash();

    public void EnemyMove(Character target, ref AiState state);
}
