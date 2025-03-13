using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject armageddonCooldownText;
    [SerializeField] private int armageddonCooldown;

    public GameObject ArmageddonCooldownText => armageddonCooldownText;
    public int ArmageddonCooldown => armageddonCooldown;

    public static EventManager Instance;

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

    public void ActivateArmageddon()
    {
        gameObject.AddComponent<ArmageddonHandler>();
    }
}