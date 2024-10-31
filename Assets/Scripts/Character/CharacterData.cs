using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;

    public float MaxHealth => maxHealth;
    public float DefaultSpeed => speed;
    public float Damage => damage;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    public float AttackRange => attackRange;
    public Transform CharacterTransform => characterTransform;

    public CharacterController CharacterController 
    { 
        get
        { 
            return characterController;
        }
    }
}
