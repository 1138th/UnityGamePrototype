using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField] private float existenceTimer;

    private float damagePerSecondTimer = 0.1f;

    private void Update()
    {
        existenceTimer -= Time.deltaTime;
        if (existenceTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            if (damagePerSecondTimer <= 0)
            {
                other.gameObject.GetComponent<Character>().HealthComponent?.TakeDamage(2);
                damagePerSecondTimer = 0.1f;
            }
            else
            {
                damagePerSecondTimer -= Time.deltaTime;
            }
        }
    }
}