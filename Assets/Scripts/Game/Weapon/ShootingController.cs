using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private BulletFactory bulletFactory;
    [SerializeField] private BulletFactory enemyBulletFactory;

    private float spawnsDeltaTime;
    private float enemyBulletSpawnsDeltaTime;
    private float reloadDeltaTime;
    private int projectilesCount = 1;

    public WeaponData WeaponData { get; private set; }
    public int BulletsShot { get; private set; }

    public void Init()
    {
        WeaponData = MetaManager.Instance.WeaponData;
        spawnsDeltaTime = WeaponData.AttackSpeed;
        reloadDeltaTime = WeaponData.ReloadTime;

        projectilesCount = MetaManager.Instance.WeaponData.ProjectilesCount;
        enemyBulletSpawnsDeltaTime = 1;
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
                Bullet[] bullets = bulletFactory.GetBullets(GameManager.Instance.CharacterFactory.Player, projectilesCount);
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

    public void ShootEnemyBullets(Character enemy)
    {
        enemyBulletSpawnsDeltaTime -= Time.deltaTime;

        if (enemyBulletSpawnsDeltaTime <= 0)
        {
            Bullet[] bullets = enemyBulletFactory.GetBullets(enemy, 1);
            foreach (var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnHit += EnemyBulletHitHandler;
            }

            enemyBulletSpawnsDeltaTime = enemy.Data.TimeBetweenAttacks;
        }
    }

    public void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= BulletHitHandler;
    }

    public void EnemyBulletHitHandler(Bullet bullet)
    {
        enemyBulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= EnemyBulletHitHandler;
    }
}