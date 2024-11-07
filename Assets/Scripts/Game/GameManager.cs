using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;

    public static GameManager Instance { get; private set; }

    public CharacterFactory CharacterFactory => characterFactory;

    private ScoreSystem scoreSystem;
    private bool isGameActive;
    private float gameSessionTime;
    private float timeBetweenEnemySpawns;

    private void Init()
    {
        scoreSystem = new ScoreSystem();
        isGameActive = false;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        if (isGameActive)
            return;
        isGameActive = true;
        gameSessionTime = 0;
        timeBetweenEnemySpawns = gameData.TimeBetweenEnemySpawn;
        
        scoreSystem.StartGame();
        
        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Init();
        player.HealthComponent.OnDeath += CharacterDeathHandler;
    }

    private void Update()
    {
        if (!isGameActive) return;
        
        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawns -= Time.deltaTime;

        if (timeBetweenEnemySpawns <= 0)
        {
            SpawnEnemies();
            timeBetweenEnemySpawns = gameData.TimeBetweenEnemySpawn;
        }
        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        } 
    }

    private void SpawnEnemies()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Init();
        enemy.HealthComponent.OnDeath += CharacterDeathHandler;
        
        float GetOffset()
        {
            bool isPositive = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return isPositive ? offset : -offset;
        }
    }

    private void CharacterDeathHandler(Character deadCharacter)
    {
        switch (deadCharacter.Type)
        {
            case CharacterType.Player:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deadCharacter.Data.ScoreValue);
                break;
        }
        
        deadCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deadCharacter);
        
        deadCharacter.HealthComponent.OnDeath -= CharacterDeathHandler;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        scoreSystem.EndGame();
        isGameActive = false;
    }
    
    private void GameVictory()
    {
        Debug.Log("Victory!");
        scoreSystem.EndGame();
        isGameActive = false;
    }
}