using UnityEngine;

public class PlayerShootingController : ShootingController
{
    private float reloadDeltaTime;
    private int projectilesCount = 1;
    private bool refillBullets = false;

    public WeaponData WeaponData { get; private set; }
    public int BulletsShot { get; private set; }

    public override void Init()
    {
        WeaponData = MetaManager.Instance.WeaponData;
        spawnsDeltaTime = WeaponData.AttackSpeed;
        reloadDeltaTime = WeaponData.ReloadTime;

        projectilesCount = MetaManager.Instance.WeaponData.ProjectilesCount;
    }

    public override void ShootBullets(Character shooter)
    {
        spawnsDeltaTime -= Time.deltaTime;

        if (BulletsShot >= WeaponData.AmmoCount)
        {
            Debug.Log("Reloading. . .");
            reloadDeltaTime -= Time.deltaTime;
            if (reloadDeltaTime <= 0)
            {
                BulletsShot = 0;
                reloadDeltaTime = WeaponData.ReloadTime;
            }
        }
        else
        {
            if (spawnsDeltaTime <= 0)
            {
                Bullet[] bullets = bulletFactory.GetBullets(shooter, projectilesCount, true);
                foreach (var bullet in bullets)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnHit += BulletHitHandler;
                }

                BulletsShot++;

                spawnsDeltaTime = WeaponData.AttackSpeed * UpgradesSystem.Instance.AttackSpeedAmp;
            }
        }
    }

    public override void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= BulletHitHandler;
    }

    public void ActivateRefillBulletsUpgrade()
    {
        refillBullets = true;
    }

    public void RefillBullet()
    {
        if (refillBullets)
        {
            BulletsShot--;
        }
    }
}