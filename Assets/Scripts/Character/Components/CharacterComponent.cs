public class CharacterComponent : ICharacterComponent
{
    private Character character;
    public Character Character { get; protected set; }

    public void Initialize(Character character)
    {
        Character = character;
    }
}
