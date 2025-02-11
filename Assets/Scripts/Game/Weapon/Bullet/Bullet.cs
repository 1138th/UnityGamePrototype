using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Character host;
    [SerializeField] private float speed;

    public event Action<Bullet> OnHit;

    private Character player;
    private Character longRangeSniperEnemy;
    private float range;
    private float penetratedEnemies;
    private float penPower;

    public void Start()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        longRangeSniperEnemy = GameManager.Instance.CharacterFactory.LongRangeSniperEnemy;
        Physics.IgnoreCollision(host.GetComponent<Collider>(), GetComponent<Collider>());

        range = MetaManager.Instance.WeaponData.AttackRange;
        penPower = MetaManager.Instance.WeaponData.PenetrationPower;
    }

    public void Update()
    {
        rb.velocity = transform.forward * speed;
        if (Vector3.Distance(transform.position, player.transform.position) > range)
        {
            OnHit?.Invoke(this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (host.gameObject.CompareTag("Player"))
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
                    OnHit?.Invoke(this);
                    penetratedEnemies = 0;
                }
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                longRangeSniperEnemy.DamageComponent.DealDamageToPlayer(player);
                OnHit?.Invoke(this);
            }
        }
    }
}