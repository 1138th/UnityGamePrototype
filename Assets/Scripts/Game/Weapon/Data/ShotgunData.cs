using UnityEngine;

public class ShotgunData : WeaponData
{
    // 1 Base DPS: 34
    // Full Base DPS: 170 (DPS * ProjectileCount)
    public ShotgunData()
    {
        WeaponId = 1;
        WeaponSprite = Resources.Load<Sprite>("Sprites/Weapons/SPAS-12_Shotgun");
        Damage = 17;
        AttackSpeed = 0.5f;
        ReloadTime = 2.5f;
        AttackRange = 50;
        ProjectilesCount = 5;
        AmmoCount = 6;
        PenetrationPower = 0;
    }
}