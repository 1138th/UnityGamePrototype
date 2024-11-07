using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float reloadTime;
    [SerializeField] private float attackRange;
    [SerializeField] private int projectilesCount;
    [SerializeField] private int ammoCount;
    [SerializeField] private int penetrationPower;

    public float Damage => damage;
    public float AttackSpeed => attackSpeed;
    public float ReloadTime => reloadTime;
    public float AttackRange => attackRange;
    public int ProjectilesCount => projectilesCount;
    public int AmmoCount => ammoCount;
    public int PenetrationPower => penetrationPower;
}