using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Character enemy1Prefab;
    [SerializeField] private Character enemy2Prefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters = new Dictionary<CharacterType, Queue<Character>>();
    //TODO: GOOD optimization => implement array if i'll know the max amount of enemies on screen
    private List<Character> activeCharacters = new List<Character>();
    
    public Character Player { get; private set; }
    public Character LongRangeSniperEnemy { get; private set; }
    public List<Character> ActiveCharacters => activeCharacters;

    public Character GetCharacter(CharacterType type)
    {
        Character character = null;
        if (disabledCharacters.ContainsKey(type))
        {
            if (disabledCharacters[type].Count > 0)
            {
                character = disabledCharacters[type].Dequeue();
            }
        }
        else
        {
            disabledCharacters.Add(type, new Queue<Character>());
        }

        if (character == null)
        {
            character = CreateCharacter(type);
        }

        activeCharacters.Add(character);
        return character;
    }

    public void ReturnCharacter(Character character)
    {
        character.gameObject.SetActive(false);
        Queue<Character> characters = disabledCharacters[character.Type];
        characters.Enqueue(character);
        
        activeCharacters.Remove(character);
    }

    private Character CreateCharacter(CharacterType type)
    {
        Character character;
        switch (type)
        {
            case CharacterType.Player:
                character = Instantiate(playerPrefab, null);
                Player = character;
                // Resources.Load<GameObject>("3DModel/AssaultRifle");
                break;
            case CharacterType.DefaultEnemy:
                character = Instantiate(enemy1Prefab, null);
                break;
            case CharacterType.LongRangeSniperEnemy:
                character = Instantiate(enemy2Prefab, null);
                LongRangeSniperEnemy = character;
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character type: " + type);
        }

        return character;
    }
}
