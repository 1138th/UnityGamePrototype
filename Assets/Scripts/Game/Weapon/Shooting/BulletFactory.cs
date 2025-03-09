using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private CharacterType shooterType;

    private List<Bullet> activeBullets = new List<Bullet>();
    private Queue<Bullet> disabledBullets = new Queue<Bullet>();
    private string bulletEntryPointPath;

    private void Start()
    {
        switch (shooterType)
        {
            case CharacterType.Player:
                bulletEntryPointPath = "CH_Rig/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/AssaultRifle/BulletEntryPoint";
                break;
            case CharacterType.Drone:
            case CharacterType.LongRangeSniperEnemy:
                bulletEntryPointPath = "BulletEntryPoint";
                break;
        }
    }

    public Bullet[] GetBullets(Character shooter, int projectilesCount, bool spread = false)
    {
        Bullet[] bullets = new Bullet[projectilesCount];

        for (int i = 0; i < projectilesCount; i++)
        {
            if (disabledBullets.Count > 0)
            {
                bullets[i] = disabledBullets.Dequeue();
                bullets[i].transform.position = GetBulletPosition(shooter);
                bullets[i].transform.rotation = shooter.transform.rotation;
                if (spread) SetBulletSpread(ref bullets[i], projectilesCount);
            }

            if (bullets[i] == null)
            {
                bullets[i] = Instantiate(bulletPrefab, GetBulletPosition(shooter), shooter.transform.rotation);
                if (spread) SetBulletSpread(ref bullets[i], projectilesCount);
            }

            activeBullets.Add(bullets[i]);
        }

        return bullets;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        Queue<Bullet> bullets = disabledBullets;
        bullets.Enqueue(bullet);

        activeBullets.Remove(bullet);
    }

    private Vector3 GetBulletPosition(Character shooter)
    {
        return shooter.gameObject.transform.Find(bulletEntryPointPath).transform.position;
    }

    private void SetBulletSpread(ref Bullet bullet, int projectilesCount)
    {
        bullet.transform.Rotate(
            0,
            Random.Range(-projectilesCount, projectilesCount),
            Random.Range(-projectilesCount, projectilesCount));
    }
}