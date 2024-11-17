using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    public event Action<Bullet> OnHit;

    private const float Speed = 100f;
    private Character player;

    public void Start()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    public void Update()
    {
        controller.Move(transform.forward * (Speed * Time.deltaTime));

        if (Vector3.Distance(transform.position, player.transform.position) > 50)
        {
            OnHit?.Invoke(this);
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            player.DamageComponent.DealDamage(hit.collider.gameObject.GetComponent<Enemy>());
        }
        OnHit?.Invoke(this);
    }
}