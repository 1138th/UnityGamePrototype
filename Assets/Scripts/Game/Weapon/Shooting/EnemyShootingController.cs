using UnityEngine;

public class EnemyShootingController : ShootingController
{
    [SerializeField] private LaserFactory laserFactory;
    [SerializeField] private LaserFactory laserTagFactory;

    public override void Init()
    {
        spawnsDeltaTime = 1.5f;
    }

    public override void ShootBullets(Character shooter)
    {
        spawnsDeltaTime -= Time.deltaTime;

        if (spawnsDeltaTime <= 0)
        {
            Bullet[] bullets = bulletFactory.GetBullets(shooter, EventManager.ProjectileAmp, true);
            foreach (var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnHit += BulletHitHandler;
            }

            spawnsDeltaTime = shooter.Data.TimeBetweenAttacks;
        }
    }

    public void PrepareLaser(Character shooter)
    {
        Laser laserTag = laserTagFactory.GetLaser(shooter);
        laserTag.gameObject.SetActive(true);
        laserTag.SetHost(shooter);

        laserTag.Destroyed += LaserTagDestroyHandler;
    }

    public void ShootLaser(Character shooter)
    {
        Laser laser = laserFactory.GetLaser(shooter);
        laser.gameObject.SetActive(true);
        laser.SetHost(shooter);

        laser.Destroyed += LaserDestroyHandler;
    }

    public override void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= BulletHitHandler;
    }

    public void LaserDestroyHandler(Laser laser)
    {
        laserFactory.ReturnLaser(laser);

        laser.Destroyed -= LaserDestroyHandler;
    }

    public void LaserTagDestroyHandler(Laser laser)
    {
        laserTagFactory.ReturnLaser(laser);

        laser.Destroyed -= LaserTagDestroyHandler;
    }
}