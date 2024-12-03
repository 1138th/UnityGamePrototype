using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private const string SAVE_NAME  = "MaxScore";
    
    [SerializeField] private Text scoreText;

    public int Score { get; private set; }
    public int MaxScore { get; private set; }

    public void StartGame()
    {
        Score = 0;
        MaxScore = PlayerPrefs.GetInt(SAVE_NAME, 0);
    }

    public void Update()
    {
        scoreText.text = $"{Score}";
    }

    public void AddScore(int earnedScore)
    {
        Score += earnedScore;
    }

    public void EndGame()
    {
        if (Score <= MaxScore) return;

        MaxScore = Score;
        PlayerPrefs.SetInt(SAVE_NAME, MaxScore);
    }
}
