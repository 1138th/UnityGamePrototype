using UnityEngine;

public abstract class ShootingController : MonoBehaviour
{
    [SerializeField] protected BulletFactory bulletFactory;

    protected float spawnsDeltaTime;

    public abstract void Init();

    public abstract void ShootBullets(Character shooter);

    public abstract void BulletHitHandler(Bullet bullet);
}