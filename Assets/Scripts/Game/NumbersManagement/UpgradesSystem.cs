using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class UpgradesSystem : MonoBehaviour
{
    public static UpgradesSystem Instance;
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private List<GameObject> upgradeButtons;

    public float ExperienceAmp { get; private set; }
    public float DamageAmp { get; private set; }
    public float AttackSpeedAmp { get; private set; }
    public float MoveSpeedAmp { get; private set; }
    public float DamageReductionAmp { get; private set; }

    private List<string> upgrades = new()
    {
        "EXP +10%",
        "DMG +10%",
        "Attack Speed +5%",
        "Move Speed +10%",
        "HP +40",
        "Armor +10",
        "+2 HP/s",
    };

    private List<string> icons = new()
    {
        "Sprites/Icons/character",
        "Sprites/Icons/gun",
        "Sprites/Icons/bullet",
        "Sprites/Icons/running",
        "Sprites/Icons/heart",
        "Sprites/Icons/shield",
        "Sprites/Icons/healthcare"
    };

    private List<string> uniqueUpgrades = new()
    {
        "Get a drone",
        "+ 1 turret for a drone",
        "Make bullets flammable"
    };

    private List<string> uniqueIcons = new()
    {
        "Sprites/Icons/drone",
        "Sprites/Icons/fire"
    };

    private Character player;
    private int[] upgradesIndex;

    private Random random = new();

    private void Awake()
    {
        ExperienceAmp = 1f;
        DamageAmp = 1f;
        AttackSpeedAmp = 1f;
        MoveSpeedAmp = 1f;
        DamageReductionAmp = 1f;

        Instance = this;
        upgradePanel.SetActive(false);
    }

    private void OnEnable()
    {
        int upgradesAmount = uniqueUpgrades.Count > 0 ? upgrades.Count + 1 : upgrades.Count;

        upgradesIndex = Enumerable.Range(0, upgradesAmount)
            .OrderBy(_ => random.Next())
            .Take(3)
            .ToArray();
        
        for (int i = 0; i < upgradesIndex.Length; i++)
        {
            if (upgradesIndex[i] == upgrades.Count)
            {
                upgradeButtons[i]
                    .transform.Find("UpgradeDescription")
                    .gameObject.GetComponent<Text>()
                    .text = uniqueUpgrades[0];

                string path = null;
                switch (uniqueUpgrades.Count)
                {
                    case 1:
                        path = uniqueIcons[1];
                        break;
                    case 2:
                        path = uniqueIcons[0];
                        break;
                    case 3:
                        path = uniqueIcons[0];
                        break;
                }
                upgradeButtons[i]
                    .transform.Find("Icon")
                    .gameObject.GetComponent<Image>()
                    .sprite = Resources.Load<Sprite>(path);
            }
            else
            {
                upgradeButtons[i]
                    .transform.Find("UpgradeDescription")
                    .gameObject.GetComponent<Text>()
                    .text = upgrades[upgradesIndex[i]];
                upgradeButtons[i]
                    .transform.Find("Icon")
                    .gameObject.GetComponent<Image>()
                    .sprite = Resources.Load<Sprite>(icons[upgradesIndex[i]]);
            }
        }
    }

    public void SelectUpgrade(int selectedUpgradeIndex)
    {
        if (player == null) 
            player = GameManager.Instance.CharacterFactory.ActiveCharacters.Find(character => 
                character.Type == CharacterType.Player);

        switch (upgradesIndex[selectedUpgradeIndex])
        {
            case 0:
                ExperienceAmp += 0.1f;
                break;
            case 1:
                DamageAmp += 0.1f;
                break;
            case 2:
                AttackSpeedAmp -= 0.05f;
                break;
            case 3:
                MoveSpeedAmp += 0.1f;
                break;
            case 4:
                player.HealthComponent.IncreaseHealth(40);
                break;
            case 5:
                DamageReductionAmp -= 0.1f;
                break;
            case 6:
                player.HealthComponent.IncreaseHpRegen(2);
                break;
            case 7:
                UniqueUpgradeHandler();
                break;
        }

        upgradePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void UniqueUpgradeHandler()
    {
        switch (uniqueUpgrades.Count)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}