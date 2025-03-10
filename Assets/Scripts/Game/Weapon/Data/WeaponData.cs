﻿public abstract class WeaponData
{
    public int WeaponId { get; protected set; }
    public string WeaponModel { get; protected set; }
    public float Damage { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float ReloadTime { get; protected set; }
    public float AttackRange { get; protected set; }
    public int ProjectilesCount { get; protected set; }
    public int AmmoCount { get; protected set; }
    public int PenetrationPower { get; protected set; }

    public void IncreaseAmmo(int ammo)
    {
        AmmoCount += ammo;
    }
}