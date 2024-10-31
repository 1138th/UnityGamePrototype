public interface ILogicComponent : ICharacterComponent
{
    public void ManualMove();

    public void AutoMove(Character targetCharacter, ref AiState state);
}
