using UnityEngine;public class CharacterComponent : ICharacterComponent
{
    public Character Character { get; protected set; }

    public void Initialize(Character character)
    {
        Character = character;
    }
}
