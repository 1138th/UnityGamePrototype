using UnityEngine;

public class FireZoneHandler : MonoBehaviour
{
    private Character player;
    private FireZone fireZonePrefab;
    private float fireZoneCooldown;
    private float fireZoneSpawnDelta;
    
    private void Awake()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        fireZonePrefab = Resources.Load<FireZone>("Low Poly Fire/Prefabs/FireZone");
        Instantiate(fireZonePrefab, player.transform.position, Quaternion.identity);
        fireZoneCooldown = EventManager.Instance.FireZoneCooldown;
    }

    private void Update()
    {
        fireZoneSpawnDelta -= Time.deltaTime;
        if (fireZoneSpawnDelta <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnFireZone();
            }

            fireZoneSpawnDelta = fireZoneCooldown;
        }
    }

    private void SpawnFireZone()
    {
        Vector3 spawnPos = new Vector3(
            player.transform.position.x + GetOffset(),
            0,
            player.transform.position.z + GetOffset());
        var zone = Instantiate(fireZonePrefab, spawnPos, Quaternion.identity);
        zone.gameObject.SetActive(true);
    }

    private float GetOffset()
    {
        bool isPositive = Random.Range(0, 100) % 2 == 0;
        float offset = Random.Range(5, 20);
        return isPositive ? offset : -offset;
    }
}