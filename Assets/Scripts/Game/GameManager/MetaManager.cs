using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaManager : MonoBehaviour
{
    public static MetaManager Instance;
    public WeaponData WeaponData { get; set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ActiveGameScene");
    }
}