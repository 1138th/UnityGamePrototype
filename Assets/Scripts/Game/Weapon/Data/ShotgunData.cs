public class ShotgunData : WeaponData
{
    // 1 Base DPS: 30
    // Full Base DPS: 150 (DPS * ProjectileCount)
    public ShotgunData()
    {
        WeaponId = 1;
        WeaponModel = "Shotgun";
        Damage = 45;
        AttackSpeed = 0.6f;
        ReloadTime = 1.5f;
        AttackRange = 30;
        ProjectilesCount = 5;
        AmmoCount = 6;
        PenetrationPower = 0;
    }
}