using UnityEngine;

public class ShotgunData : WeaponData
{
    // 1 Base DPS: 30
    // Full Base DPS: 150 (DPS * ProjectileCount)
    public ShotgunData()
    {
        WeaponId = 1;
        WeaponSprite = Resources.Load<Sprite>("Sprites/Weapons/SPAS-12_Shotgun");
        Damage = 45;
        AttackSpeed = 1.5f;
        ReloadTime = 2.5f;
        AttackRange = 50;
        ProjectilesCount = 5;
        AmmoCount = 6;
        PenetrationPower = 0;
    }
}