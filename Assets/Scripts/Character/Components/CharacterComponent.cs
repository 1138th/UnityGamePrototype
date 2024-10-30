public class CharacterComponent : ICharacterComponent
{
    private Character character;
    public Character Character { get; set; }

    public void Initialize(Character character)
    {
        this.character = character;
    }
}
