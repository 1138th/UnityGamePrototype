using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private int sessionTimeMinutes = 15;
    [SerializeField] private int spawnsAmountForMultiplierToIncrease = 20;
    [SerializeField] private int maxEnemiesPerSpawn = 10;
    [SerializeField] private float timeBetweenEnemySpawn = 1.5f;
    [SerializeField] private float minSpawnOffset = 15;
    [SerializeField] private float maxSpawnOffset = 30;
    
    public int SessionTimeMinutes => sessionTimeMinutes;
    public int SessionTimeSeconds => sessionTimeMinutes * 60;
    public int SpawnsAmountForMultiplierToIncrease => spawnsAmountForMultiplierToIncrease;
    public int MaxEnemiesPerSpawn => maxEnemiesPerSpawn;
    public float TimeBetweenEnemySpawn => timeBetweenEnemySpawn;
    public float MinSpawnOffset => minSpawnOffset;
    public float MaxSpawnOffset => maxSpawnOffset;
}
