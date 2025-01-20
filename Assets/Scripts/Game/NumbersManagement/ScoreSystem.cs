using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private const string PrefsMaxScoreConst  = "MaxScore";
    
    [SerializeField] private Text scoreText;
    [SerializeField] private Slider expBar;
    [SerializeField] private GameObject upgradePanel;

    public float Score { get; private set; }
    public float MaxScore { get; private set; }
    public int PlayerLevel { get; private set; }

    private long expToNextLevel;

    public void StartGame()
    {
        Score = 0;
        PlayerLevel = 1;
        MaxScore = PlayerPrefs.GetFloat(PrefsMaxScoreConst, 0);

        expToNextLevel = GetExpToNextLevel();
        expBar.value = 0;
        expBar.maxValue = expToNextLevel;
    }

    public void Update()
    {
        if (Score >= expToNextLevel)
        {
            PlayerLevel++;
            expToNextLevel = GetExpToNextLevel();
            expBar.minValue = Score;
            expBar.maxValue = expToNextLevel;

            upgradePanel.SetActive(true);
            Time.timeScale = 0;
        }

        scoreText.text = $"{Score}";
        expBar.value = Score;
    }

    private long GetExpToNextLevel()
    {
        return (long) Math.Floor(10 * PlayerLevel * Math.Pow(PlayerLevel, 1.5));
    }

    public void AddScore(int earnedScore)
    {
        Score += earnedScore * UpgradesSystem.Instance.ExperienceAmp;
    }

    public void EndGame()
    {
        if (Score <= MaxScore) return;

        MaxScore = Score;
        PlayerPrefs.SetFloat(PrefsMaxScoreConst, MaxScore);
    }
}
