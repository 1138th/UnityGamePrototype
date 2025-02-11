using UnityEngine;

public class EnemyBullet : Bullet
{
    private Character player;
    private Character longRangeSniperEnemy;

    public override void Start()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        longRangeSniperEnemy = GameManager.Instance.CharacterFactory.LongRangeSniperEnemy;
        Physics.IgnoreCollision(longRangeSniperEnemy.GetComponent<Collider>(), GetComponent<Collider>());
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
        if (other.gameObject.CompareTag("Player"))
        {
            longRangeSniperEnemy.DamageComponent.DealDamageToPlayer(player);
            InvokeOnHit(this);
        }
    }
}