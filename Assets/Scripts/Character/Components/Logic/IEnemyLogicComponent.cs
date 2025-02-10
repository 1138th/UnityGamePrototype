public interface IEnemyLogicComponent : ILogicComponent
{
    public void EnemyMove(Character target, ref AiState state);
}