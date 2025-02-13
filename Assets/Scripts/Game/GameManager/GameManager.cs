using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private CharacterSpawnController spawnController;
    [SerializeField] private PlayerShootingController playerShootingController;
    [SerializeField] private DroneShootingController droneShootingController;
    [SerializeField] private EnemyShootingController enemyShootingController;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    private bool isGameActive;
    private bool isGamePaused;
    private bool isEnemiesSpawnEnabled = true;
    private float gameSessionTime;
    private int enemiesKilled;

    public CharacterFactory CharacterFactory => characterFactory;
    public PlayerShootingController PlayerShootingController => playerShootingController;
    public DroneShootingController DroneShootingController => droneShootingController;
    public EnemyShootingController EnemyShootingController => enemyShootingController;
    public int EnemiesKilled => enemiesKilled;
    public bool IsGameActive => isGameActive;

    private void Init()
    {
        spawnController.Init();
        playerShootingController.Init();
        enemyShootingController.Init();
        isGameActive = false;
        isGamePaused = false;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (isGameActive) return;
        isGameActive = true;
        gameSessionTime = 0;
        enemiesKilled = 0;

        scoreSystem.StartGame();

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Init();
        player.HealthComponent.OnDeath += CharacterDeathHandler;
    }

    public void Restart()
    {
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            isGamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    private void Update()
    {
        if (!isGameActive) return;

        gameSessionTime += Time.deltaTime;

        if (isEnemiesSpawnEnabled) spawnController.ExecuteSpawnEnemiesLogic();

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameOver("Victory!");
        }
    }

    public void EnableEnemiesSpawn()
    {
        isEnemiesSpawnEnabled = !isEnemiesSpawnEnabled;
    }

    public void CharacterDeathHandler(Character deadCharacter)
    {
        switch (deadCharacter.Type)
        {
            case CharacterType.Player:
                GameOver("Game Over!");
                break;
            case CharacterType.DefaultEnemy:
            case CharacterType.LongRangeSniperEnemy:
                scoreSystem.AddScore(deadCharacter.Data.ScoreValue);
                enemiesKilled++;
                if (enemiesKilled % 3 == 0) playerShootingController.RefillBullet();
                break;
        }

        characterFactory.ReturnCharacter(deadCharacter);

        deadCharacter.HealthComponent.OnDeath -= CharacterDeathHandler;
    }

    private void GameOver(string message)
    {
        Debug.Log(message);
        scoreSystem.EndGame();
        isGameActive = false;
        
        characterFactory.ActiveCharacters.ForEach(character =>
        {
            if (character.Type == CharacterType.Player) return;
            character.gameObject.SetActive(false);
            Destroy(character.gameObject);
        });
        characterFactory.ActiveCharacters.RemoveAll(character => character.Type != CharacterType.Player);
        gameOverMenu.SetActive(true);
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }
}