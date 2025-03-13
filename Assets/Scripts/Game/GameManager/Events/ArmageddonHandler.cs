using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmageddonHandler : MonoBehaviour
{
    private GameManager gm;
    
    private GameObject armageddonCooldownText;
    private Text armageddonCooldownTextfield;
    private int armageddonCooldown;
    private float armageddonCooldownTimer;

    private void Awake()
    {
        gm = GameManager.Instance;

        armageddonCooldown = EventManager.Instance.ArmageddonCooldown;
        armageddonCooldownText = EventManager.Instance.ArmageddonCooldownText;

        armageddonCooldownTimer = armageddonCooldown;
        armageddonCooldownText.SetActive(true);

        armageddonCooldownTextfield = armageddonCooldownText.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        armageddonCooldownTimer -= Time.deltaTime;
        armageddonCooldownTextfield.text = Mathf.FloorToInt(armageddonCooldownTimer).ToString();

        if (armageddonCooldownTimer <= 0)
        {
            ExecuteArmageddon();
            armageddonCooldownTimer = armageddonCooldown;
        }
    }

    private void ExecuteArmageddon()
    {
        List<Character> characters = gm.CharacterFactory.ActiveCharacters;
        for (int i = characters.Count - 1; i >= 0; i--)
        {
            if (characters[i].Type == CharacterType.Player) continue;
            characters[i].HealthComponent.ExecuteDeath();
        }
    }
}