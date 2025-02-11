using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;

    public event Action<Bullet> OnHit;

    protected const float BulletDespawnRange = 50;

    public abstract void Start();

    public abstract void Update();

    public abstract void OnTriggerEnter(Collider other);

    protected void InvokeOnHit(Bullet bullet)
    {
        OnHit?.Invoke(bullet);
    }
}