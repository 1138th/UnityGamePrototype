using UnityEngine;

public class DroneBullet : Bullet
{
    private Character player;
    private Character drone;

    public override void Start()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        drone = GameManager.Instance.CharacterFactory.Drone;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(drone.GetComponent<Collider>(), GetComponent<Collider>());
    }

    public override void Update()
    {
        rb.velocity = transform.forward * speed;
        if (Vector3.Distance(transform.position, player.transform.position) > BulletDespawnRange)
        {
            InvokeOnHit(this);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            drone.DamageComponent.DealDamage(other.gameObject.GetComponent<Enemy>(),
                drone.Data.Damage * UpgradesSystem.Instance.DamageAmp);
            InvokeOnHit(this);
        }
    }
}