using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private BulletFactory bulletFactory;

    private float spawnsDeltaTime;
    private float reloadDeltaTime;
    public int BulletsShot { get; private set; }

    public void Init()
    {
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
                Bullet bullet = bulletFactory.GetBullet();
                bullet.gameObject.SetActive(true);
                bullet.OnHit += BulletHitHandler;
                BulletsShot++;

                spawnsDeltaTime = WeaponData.AttackSpeed;
            }
        }
    }

    public void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= BulletHitHandler;
    }
}