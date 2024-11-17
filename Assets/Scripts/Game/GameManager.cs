using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private BulletFactory bulletFactory;
    [SerializeField] private CharacterSpawnController spawnController;
    [SerializeField] private ShootingController shootingController;
    [SerializeField] private ScoreSystem scoreSystem;

    private bool isGameActive;
    private float gameSessionTime;

    public CharacterFactory CharacterFactory => characterFactory;
    public ShootingController ShootingController => shootingController;
    public float SessionTime => gameSessionTime;

    private void Init()
    {
        spawnController.Init();
        shootingController.Init();
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
            GameOver("Victory!");
        }
    }

    public void CharacterDeathHandler(Character deadCharacter)
    {
        switch (deadCharacter.Type)
        {
            case CharacterType.Player:
                GameOver("Game Over!");
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deadCharacter.Data.ScoreValue);
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
    }
}