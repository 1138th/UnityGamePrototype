using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject armageddonCooldownText;
    [SerializeField] private int armageddonCooldown;

    public static EventManager Instance;
    private Text armageddonCooldownTextfield;

    private bool isArmageddonActive = false;
    private float armageddonCooldownTimer;

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
        armageddonCooldownTimer = armageddonCooldown;
        armageddonCooldownTextfield = armageddonCooldownText.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.IsGameActive) return;

        if (isArmageddonActive)
        {
            armageddonCooldownTimer -= Time.deltaTime;
            armageddonCooldownTextfield.text = Mathf.FloorToInt(armageddonCooldownTimer).ToString();

            if (armageddonCooldownTimer <= 0)
            {
                ExecuteArmageddon();
                armageddonCooldownTimer = armageddonCooldown;
            }
        }
    }

    private void ExecuteArmageddon()
    {
        List<Character> characters = gameManager.CharacterFactory.ActiveCharacters;
        for (int i = characters.Count - 1; i >= 0; i--)
        {
            if (characters[i].Type == CharacterType.Player) continue;
            characters[i].HealthComponent.ExecuteDeath();
        }
    }

    public void ActivateArmageddon()
    {
        isArmageddonActive = true;
        armageddonCooldownText.SetActive(true);
    }
}