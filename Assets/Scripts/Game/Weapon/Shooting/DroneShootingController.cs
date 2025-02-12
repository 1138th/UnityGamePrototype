using UnityEngine;

public class DroneShootingController : ShootingController
{
    [SerializeField] private int projectilesNumber;

    public int ProjectilesNumber => projectilesNumber;

    private float spawnsDelta;
    private int rotationDelta;

    public override void Init()
    {
        spawnsDeltaTime = GameManager.Instance.CharacterFactory.Drone.Data.TimeBetweenAttacks;
        spawnsDelta = spawnsDeltaTime;
        rotationDelta = 360 / projectilesNumber;
    }

    public override void ShootBullets(Character shooter)
    {
        spawnsDelta -= Time.deltaTime;

        if (spawnsDelta <= 0)
        {
            Bullet[] bullets = bulletFactory.GetBullets(shooter, projectilesNumber);
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].gameObject.SetActive(true);

                bullets[i].transform.Rotate(new Vector3(0, i * rotationDelta, 0));
                
                bullets[i].OnHit += BulletHitHandler;
            }

            spawnsDelta = spawnsDeltaTime;
        }
    }

    public override void BulletHitHandler(Bullet bullet)
    {
        bulletFactory.ReturnBullet(bullet);

        bullet.OnHit -= BulletHitHandler;
    }

    public void ChangeProjectilesNumber(int number)
    {
        projectilesNumber = number;
        rotationDelta = 360 / projectilesNumber;
    }
}