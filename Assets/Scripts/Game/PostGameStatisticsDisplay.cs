using UnityEngine;
using UnityEngine.UI;

public class PostGameStatisticsDisplay : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject postGameStatisticsPanel;

    private void Start()
    {
        SetNumber(gameManager.EnemiesKilled, postGameStatisticsPanel.transform.Find("EnemiesKilled").gameObject);
        SetNumber(scoreSystem.Score, postGameStatisticsPanel.transform.Find("Score").gameObject);
        SetNumber(scoreSystem.MaxScore, postGameStatisticsPanel.transform.Find("MaxScore").gameObject);
    }

    private void SetNumber(int number, GameObject panel)
    {
        panel.transform.Find("Number").GetComponent<Text>().text = number.ToString();
    }
}