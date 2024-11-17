public interface ILogicComponent : ICharacterComponent
{
    public void PlayerMove(Character target);

    public void EnemyMove(Character target, ref AiState state);
}
