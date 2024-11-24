using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    private List<Bullet> activeBullets = new List<Bullet>();
    private Queue<Bullet> disabledBullets = new Queue<Bullet>();

    private int projectilesCount = 1;

    private void Start()
    {
        projectilesCount = MetaManager.Instance.WeaponData.ProjectilesCount;
    }

    public Bullet[] GetBullets()
    {
        Bullet[] bullets = new Bullet[projectilesCount];

        for (int i = 0; i < projectilesCount; i++)
        {
            Character player = GameManager.Instance.CharacterFactory.Player;

            if (disabledBullets.Count > 0)
            {
                bullets[i] = disabledBullets.Dequeue();
                bullets[i].transform.position = GetBulletPosition(player);
                bullets[i].transform.rotation = player.transform.rotation;
                SetBulletSpread(ref bullets[i]);
            }

            if (bullets[i] == null)
            {
                bullets[i] = Instantiate(bulletPrefab, GetBulletPosition(player), player.transform.rotation);
                SetBulletSpread(ref bullets[i]);
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

    private void SetBulletSpread(ref Bullet bullet)
    {
        bullet.transform.Rotate(
            0,
            Random.Range(-projectilesCount, projectilesCount),
            Random.Range(-projectilesCount, projectilesCount));
    }
}