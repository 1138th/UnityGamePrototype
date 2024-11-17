using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private BulletFactory bulletFactory;

    private float spawnsDeltaTime;
    private int spawnsExecuted;

    public void Init()
    {
        spawnsDeltaTime = WeaponData.AttackSpeed;
    }

    public void ShootBullet()
    {
        spawnsDeltaTime -= Time.deltaTime;

        if (spawnsDeltaTime <= 0)
        {
            Bullet bullet = bulletFactory.GetBullet();
            bullet.gameObject.SetActive(true);
            bullet.OnHit += BulletHitHandler;

            spawnsDeltaTime = WeaponData.AttackSpeed;
        }
    }

    public void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);
        
        bullet.OnHit -= BulletHitHandler;
    }
}