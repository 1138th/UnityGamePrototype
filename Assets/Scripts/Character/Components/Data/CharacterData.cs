using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int scoreValue;

    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Slider healthBar;

    public float MaxHealth => maxHealth;
    public float DefaultSpeed => speed;
    public float DashSpeed => dashSpeed;
    public float DashTime => dashTime;
    public float Damage => damage;
    public float AttackRange => attackRange;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    public int ScoreValue => scoreValue;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;
    public Slider HealthBar => healthBar;
}
