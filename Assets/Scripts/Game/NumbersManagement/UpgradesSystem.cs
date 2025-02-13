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

    private Character player;
    private int[] upgradesIndex;
    private int uniqueUpgradeIndex;
    private bool upgradeDrone;
    private bool upgradeUnique;

    private Random random = new();

    private const string UniqueUpgrade1 = "Refill a bullet for each 3 kills";
    private const string UniqueUpgrade2 = "x2 ammo amount";
    private const string UniqueUpgrade3 = "Armageddon\n(once per 75s)";

    private List<string> repetitiveUpgrades = new()
    {
        "EXP +10%",
        "DMG +10%",
        "Attack Speed +5%",
        "Move Speed +10%",
        "HP +40",
        "DMG Reduction +10%",
        "+2 HP/s"
    };

    private List<string> repetitiveIcons = new()
    {
        "Sprites/Icons/character",
        "Sprites/Icons/gun",
        "Sprites/Icons/bullet",
        "Sprites/Icons/running",
        "Sprites/Icons/heart",
        "Sprites/Icons/shield",
        "Sprites/Icons/healthcare"
    };

    private List<string> droneUpgrades = new()
    {
        "Get a drone that shoots enemies",
        "x2 drone projectiles"
    };

    private List<string> uniqueUpgrades = new()
    {
        UniqueUpgrade1,
        UniqueUpgrade2,
        UniqueUpgrade3
    };

    private List<string> uniqueIcons = new()
    {
        "Sprites/Icons/infinity",
        "Sprites/Icons/ammunition",
        "Sprites/Icons/nuclear-explosion"
    };

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
        upgradeDrone = false;
        upgradeUnique = false;
        int upgradesAmount = droneUpgrades.Count > 0 || uniqueUpgrades.Count > 0
            ? repetitiveUpgrades.Count + 1
            : repetitiveUpgrades.Count;
        if (droneUpgrades.Count > 0)
        {
            upgradeDrone = true;
        }

        if (uniqueUpgrades.Count > 0)
        {
            upgradeUnique = true;
        }

        upgradesIndex = Enumerable.Range(0, upgradesAmount)
            .OrderBy(_ => random.Next())
            .Take(3)
            .ToArray();

        for (int i = 0; i < upgradesIndex.Length; i++)
        {
            if (upgradesIndex[i] == repetitiveUpgrades.Count) // DRONE OR UNIQUE UPGRADES
            {
                if (upgradeDrone && upgradeUnique)
                {
                    upgradeDrone = random.NextDouble() >= 0.5;
                    upgradeUnique = !upgradeDrone;
                }

                if (upgradeDrone) // DRONE UPGRADES
                {
                    FillUpgradeInfo(i, droneUpgrades[0], "Sprites/Icons/drone");
                }

                if (upgradeUnique) // UNIQUE UPGRADES
                {
                    uniqueUpgradeIndex = random.Next(uniqueUpgrades.Count);

                    FillUpgradeInfo(i,
                        uniqueUpgrades[uniqueUpgradeIndex],
                        uniqueIcons[uniqueUpgradeIndex]);
                }
            }
            else // REPETITIVE UPGRADES
            {
                FillUpgradeInfo(i,
                    repetitiveUpgrades[upgradesIndex[i]],
                    repetitiveIcons[upgradesIndex[i]]);
            }
        }
    }

    private void FillUpgradeInfo(int buttonIndex, string upgradeDescription, string iconPath)
    {
        upgradeButtons[buttonIndex]
            .transform.Find("UpgradeDescription")
            .gameObject.GetComponent<Text>()
            .text = upgradeDescription;
        upgradeButtons[buttonIndex]
            .transform.Find("Icon")
            .gameObject.GetComponent<Image>()
            .sprite = Resources.Load<Sprite>(iconPath);
    }

    public void SelectUpgrade(int selectedUpgradeIndex)
    {
        if (player == null) player = GameManager.Instance.CharacterFactory.Player;

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
                if (upgradeDrone) DroneUpgradeHandler();
                if (upgradeUnique) UniqueUpgradeHandler();
                break;
        }

        upgradePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void DroneUpgradeHandler()
    {
        switch (droneUpgrades.Count)
        {
            case 2:
                Character drone = GameManager.Instance.CharacterFactory.CreateCharacter(CharacterType.Drone);
                drone.gameObject.SetActive(true);
                drone.Init();
                gameManager.DroneShootingController.Init();

                break;
            case 1:
                GameManager.Instance.DroneShootingController.ChangeProjectilesNumber(
                    GameManager.Instance.DroneShootingController.ProjectilesNumber * 2);
                break;
        }

        droneUpgrades.RemoveAt(0);
    }

    private void UniqueUpgradeHandler()
    {
        switch (uniqueUpgrades[uniqueUpgradeIndex])
        {
            case UniqueUpgrade1:
                gameManager.PlayerShootingController.ActivateRefillBulletsUpgrade();
                break;
            case UniqueUpgrade2:
                MetaManager.Instance.WeaponData.IncreaseAmmo(MetaManager.Instance.WeaponData.AmmoCount);
                break;
            case UniqueUpgrade3:
                EventManager.Instance.ActivateArmageddon();
                break;
        }

        uniqueUpgrades.RemoveAt(uniqueUpgradeIndex);
        uniqueIcons.RemoveAt(uniqueUpgradeIndex);
    }
}