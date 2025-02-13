using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private GameData data;
    [SerializeField] private GameManager gameManager;

    private float currentSessionTime;
    private float sessionTime;

    private void Awake()
    {
        sessionTime = data.SessionTimeSeconds;
    }

    private void FixedUpdate()
    {
        if (!gameManager.IsGameActive) return;

        currentSessionTime = sessionTime - Time.timeSinceLevelLoad;
        int minutes = Mathf.FloorToInt(currentSessionTime / 60f);
        int seconds = Mathf.FloorToInt(currentSessionTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}