using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaManager : MonoBehaviour
{
    public static MetaManager Instance;
    public WeaponData WeaponData { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ActiveGameScene");
    }
}