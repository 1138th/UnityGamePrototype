using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    private List<Bullet> activeBullets = new List<Bullet>();
    private Queue<Bullet> disabledBullets = new Queue<Bullet>();

    public Bullet[] GetBullets(Character shooter, int projectilesCount)
    {
        Bullet[] bullets = new Bullet[projectilesCount];

        for (int i = 0; i < projectilesCount; i++)
        {
            if (disabledBullets.Count > 0)
            {
                bullets[i] = disabledBullets.Dequeue();
                bullets[i].transform.position = GetBulletPosition(shooter);
                bullets[i].transform.rotation = shooter.transform.rotation;
                SetBulletSpread(ref bullets[i], projectilesCount);
            }

            if (bullets[i] == null)
            {
                bullets[i] = Instantiate(bulletPrefab, GetBulletPosition(shooter), shooter.transform.rotation);
                SetBulletSpread(ref bullets[i], projectilesCount);
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

    private Vector3 GetBulletPosition(Character player)
    {
        return new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
    }

    private void SetBulletSpread(ref Bullet bullet, int projectilesCount)
    {
        bullet.transform.Rotate(
            0,
            Random.Range(-projectilesCount, projectilesCount),
            Random.Range(-projectilesCount, projectilesCount));
    }
}