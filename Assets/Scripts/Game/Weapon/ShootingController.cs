using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private BulletFactory bulletFactory;

    private float spawnsDeltaTime;
    private float reloadDeltaTime;

    public WeaponData WeaponData { get; private set; }
    public int BulletsShot { get; private set; }

    public void Init()
    {
        WeaponData = MetaManager.Instance.WeaponData;
        spawnsDeltaTime = WeaponData.AttackSpeed;
        reloadDeltaTime = WeaponData.ReloadTime;
    }

    public void ShootBullets()
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
                Bullet[] bullets = bulletFactory.GetBullets();
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

    public void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= BulletHitHandler;
    }
}