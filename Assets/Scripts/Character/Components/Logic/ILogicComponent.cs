public interface ILogicComponent : ICharacterComponent
{
    public void ManualMove(Character target);

    public void AutoMove(Character target, ref AiState state);
}
