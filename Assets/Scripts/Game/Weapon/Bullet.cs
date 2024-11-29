using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public event Action<Bullet> OnHit;

    private const float Speed = 100;
    private Character player;
    private float range;
    private float penetratedEnemies;
    private float penPower;

    public void Start()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        range = MetaManager.Instance.WeaponData.AttackRange;
        penPower = MetaManager.Instance.WeaponData.PenetrationPower;
    }

    public void Update()
    {
        rb.velocity = transform.forward * Speed;
        if (Vector3.Distance(transform.position, player.transform.position) > range)
        {
            OnHit?.Invoke(this);
        }
    }

    public void OnTriggerEnter(Collider other)
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
}