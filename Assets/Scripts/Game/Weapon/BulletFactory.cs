using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    private List<Bullet> activeBullets = new List<Bullet>();
    private Queue<Bullet> disabledBullets = new Queue<Bullet>();

    public List<Bullet> ActiveBullets => activeBullets;

    public Bullet GetBullet()
    {
        Bullet bullet = null;
        Character player = GameManager.Instance.CharacterFactory.Player;

        if (disabledBullets.Count > 0)
        {
            bullet = disabledBullets.Dequeue();
            bullet.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1,
                player.transform.position.z);
            bullet.transform.rotation = player.transform.rotation;
        }

        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab,
                new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z),
                player.transform.rotation);
        }

        activeBullets.Add(bullet);
        return bullet;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        Queue<Bullet> bullets = disabledBullets;
        bullets.Enqueue(bullet);

        activeBullets.Remove(bullet);
    }
}