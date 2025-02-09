using UnityEngine;

public class CharacterComponent : MonoBehaviour, ICharacterComponent
{
    public Character Character { get; protected set; }

    public void Initialize(Character character)
    {
        Character = character;
    }
}
