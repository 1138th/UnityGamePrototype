using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Character dronePrefab;
    [SerializeField] private Character enemy1Prefab;
    [SerializeField] private Character enemy2Prefab;
    [SerializeField] private Character enemy3Prefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters = new Dictionary<CharacterType, Queue<Character>>();
    private List<Character> activeCharacters = new List<Character>();
    
    public Character Player { get; private set; }
    public Character Drone { get; private set; }
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

    public Character CreateCharacter(CharacterType type)
    {
        Character character;
        switch (type)
        {
            case CharacterType.Player:
                character = Instantiate(playerPrefab, null);
                Player = character;
                break;
            case CharacterType.Drone:
                character = Instantiate(dronePrefab, null);
                Drone = character;
                break;
            case CharacterType.DefaultEnemy:
                character = Instantiate(enemy1Prefab, null);
                break;
            case CharacterType.LongRangeSniperEnemy:
                character = Instantiate(enemy2Prefab, null);
                LongRangeSniperEnemy = character;
                break;
            case CharacterType.LaserEnemy:
                character = Instantiate(enemy3Prefab, null);
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character type: " + type);
        }

        return character;
    }
}
