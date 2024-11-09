using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private CharacterSpawnController spawnController;
    [SerializeField] private ScoreSystem scoreSystem;

    private bool isGameActive;
    private float gameSessionTime;
    
    public CharacterFactory CharacterFactory => characterFactory;
    public float SessionTime => gameSessionTime;

    private void Init()
    {
        spawnController.Init();
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

        spawnController.ExecuteSpawnEnemiesLogic();
        
        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        } 
    }

    public void CharacterDeathHandler(Character deadCharacter)
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