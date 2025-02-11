using UnityEngine;

public class PlayerBullet : Bullet
{
    private Character player;
    private float penetratedEnemies;
    private float penPower;

    public override void Start()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        penPower = MetaManager.Instance.WeaponData.PenetrationPower;
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
            player.DamageComponent.DealDamage(other.gameObject.GetComponent<Enemy>());
            if (penPower > 0 && penetratedEnemies < penPower)
            {
                penetratedEnemies++;
            }
            else
            {
                InvokeOnHit(this);
                penetratedEnemies = 0;
            }
        }
    }
}