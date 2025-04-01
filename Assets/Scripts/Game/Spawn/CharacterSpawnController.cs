using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSpawnController : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;

    private float spawnsDeltaTime;
    private int spawnMultiplier;
    private int spawnsExecuted;

    public void Init()
    {
        spawnMultiplier = 1;
        spawnsDeltaTime = gameData.TimeBetweenEnemySpawn;
    }

    public void ExecuteSpawnEnemiesLogic()
    {
        spawnsDeltaTime -= Time.deltaTime;

        if (spawnsDeltaTime <= 0)
        {
            CalculateSpawnMultiplier();
            SpawnEnemies(spawnMultiplier * EventManager.DmgSpawnAmp);
            spawnsDeltaTime = gameData.TimeBetweenEnemySpawn;
        }
    }

    private void CalculateSpawnMultiplier()
    {
        spawnsExecuted++;
        if (spawnsExecuted >= gameData.SpawnsAmountForMultiplierToIncrease)
        {
            if (spawnMultiplier < gameData.MaxEnemiesPerSpawn)
            {
                spawnMultiplier++;
            }
            spawnsExecuted = 0;
        }
    }

    private void SpawnEnemies(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Character enemy = characterFactory.GetCharacter(RandomizeEnemyType());

            Vector3 playerPosition = characterFactory.Player.transform.position;
            enemy.transform.position = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
            enemy.gameObject.SetActive(true);

            enemy.Init();
            enemy.HealthComponent.OnDeath += GameManager.Instance.CharacterDeathHandler;
        }
    }

    /// <summary>
    /// Evenly redistributes chance of spawning any type of enemy 
    /// </summary>
    private CharacterType RandomizeEnemyType()
    {
        return (CharacterType) Random.Range(3, Enum.GetValues(typeof(CharacterType)).Length);
    }

    private float GetOffset()
    {
        bool isPositive = Random.Range(0, 100) % 2 == 0;
        float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
        return isPositive ? offset : -offset;
    }
}