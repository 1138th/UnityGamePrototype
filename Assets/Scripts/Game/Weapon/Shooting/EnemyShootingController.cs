using UnityEngine;

public class EnemyShootingController : ShootingController
{
    public override void Init()
    {
        spawnsDeltaTime = 1.5f;
    }

    public override void ShootBullets(Character shooter)
    {
        spawnsDeltaTime -= Time.deltaTime;

        if (spawnsDeltaTime <= 0)
        {
            Bullet[] bullets = bulletFactory.GetBullets(shooter, 1);
            foreach (var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnHit += BulletHitHandler;
            }

            spawnsDeltaTime = shooter.Data.TimeBetweenAttacks;
        }
    }

    public override void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= BulletHitHandler;
    }
}