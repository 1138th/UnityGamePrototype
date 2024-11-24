using UnityEngine;

public class AssaultRifleData : WeaponData
{
    // Base DPS: 150
    public AssaultRifleData()
    {
        WeaponId = 0;
        WeaponSprite = Resources.Load<Sprite>("Sprites/Weapons/HK_416_Assault_Rifle");
        Damage = 15;
        AttackSpeed = 0.1f;
        ReloadTime = 2;
        AttackRange = 100;
        ProjectilesCount = 1;
        AmmoCount = 31;
        PenetrationPower = 0;
    }
}