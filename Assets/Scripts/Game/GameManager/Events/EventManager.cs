using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    public static int DmgSpawnAmp { get; private set; }
    public static int ProjectileAmp { get; private set; }
    public static float MoveSpeedAmp { get; private set; }
    public static float ReloadTimeAmp { get; private set; }
    public static bool WeaponEnabled { get; private set; }
    public static bool AutoAimEnabled { get; private set; }

    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text eventText;
    [SerializeField] private GameObject armageddonCooldownText;
    [SerializeField] private float fireZoneCooldown;
    [SerializeField] private int armageddonCooldown;
    [SerializeField] private float eventCooldown;
    [SerializeField] private float eventDuration;

    public GameObject ArmageddonCooldownText => armageddonCooldownText;
    public int ArmageddonCooldown => armageddonCooldown;
    public float FireZoneCooldown => fireZoneCooldown;

    private int currentEventId = -1;
    private float eventCooldownTimer;
    private float eventDurationTimer;
    private bool isEventActive = false;

    private const string Event1 = "Double the enemies, double your damage";
    private const string Event2 = "Double projectiles, double the fun";
    private const string Event3 = "Time moves faster";
    private const string Event4 = "Weapon is disabled, you move faster";
    private const string Event5 = "Speed -90%, you have infinite ammo";
    private const string Event6 = "Disable auto aim";
    private const string Event7 = "Spawning fire zones";

    private List<string> eventsList = new() { Event1, Event2, Event3, Event4, Event5, Event6, Event7 };

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

        eventCooldownTimer = eventCooldown;
        eventDurationTimer = eventDuration;

        DmgSpawnAmp = 1;
        ProjectileAmp = 1;
        MoveSpeedAmp = 1;
        ReloadTimeAmp = 1;
        WeaponEnabled = true;
        AutoAimEnabled = true;
        eventText.text = "New event will start soon";
    }

    private void FixedUpdate()
    {
        if (isEventActive)
        {
            eventDurationTimer -= Time.fixedDeltaTime;
            if (eventDurationTimer <= 0)
            {
                isEventActive = false;
                eventDurationTimer = eventDuration;

                StartEndEvent();
            }
        }
        else
        {
            eventCooldownTimer -= Time.fixedDeltaTime;
            eventText.text = $"New event in {eventCooldownTimer:00} seconds";
            if (eventCooldownTimer <= 0)
            {
                isEventActive = true;
                eventCooldownTimer = eventCooldown;

                StartEndEvent();
            }
        }
    }

    private void StartEndEvent()
    {
        int eventIndex = Random.Range(0, eventsList.Count);
        if (isEventActive)
        {
            eventText.text = eventsList[eventIndex];
            currentEventId = eventIndex;
        }
        else
        {
            eventIndex = currentEventId;
        }


        switch (eventsList[eventIndex])
        {
            case Event1:
                DmgSpawnAmp = isEventActive ? 2 : 1;
                break;
            case Event2:
                if (isEventActive)
                {
                    ProjectileAmp = 2;
                    gameManager.DroneShootingController
                        .ChangeProjectilesNumber(gameManager.DroneShootingController.ProjectilesNumber * 2);
                }
                else
                {
                    ProjectileAmp = 1;
                    gameManager.DroneShootingController
                        .ChangeProjectilesNumber(gameManager.DroneShootingController.ProjectilesNumber / 2);
                }

                break;
            case Event3:
                Time.timeScale = isEventActive ? 3 : 1;
                break;
            case Event4:
                WeaponEnabled = !isEventActive;
                MoveSpeedAmp = isEventActive ? 2 : 1;
                break;
            case Event5:
                MoveSpeedAmp = isEventActive ? 0.1f : 1;
                ReloadTimeAmp = isEventActive ? 0 : 1;
                break;
            case Event6:
                AutoAimEnabled = !isEventActive;
                break;
            case Event7:
                if (isEventActive) gameObject.AddComponent<FireZoneHandler>();
                else Destroy(GetComponent<FireZoneHandler>());
                break;
        }
    }

    public void ActivateArmageddon()
    {
        gameObject.AddComponent<ArmageddonHandler>();
    }
}