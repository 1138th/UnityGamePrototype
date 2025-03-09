using UnityEngine;

public class LaserProjectile : Laser
{
    private float damagePerSecondTimer = 0.1f;

    protected override void Update()
    {
        if (!Host || !Host.isActiveAndEnabled)
        {
            InvokeDestroyed(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (damagePerSecondTimer <= 0)
            {
                Host.DamageComponent.DealDamageToPlayer(GameManager.Instance.CharacterFactory.Player);
                damagePerSecondTimer = 0.1f;
            }
            else
            {
                damagePerSecondTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            damagePerSecondTimer = 0.1f;
        }
    }
}