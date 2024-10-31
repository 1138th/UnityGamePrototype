public interface ILogicComponent : ICharacterComponent
{
    public void ManualMove();

    public void AutoMove(Character target, ref AiState state);
}
