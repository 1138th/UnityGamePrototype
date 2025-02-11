public class AssaultRifleData : WeaponData
{
    // Base DPS: 150
    public AssaultRifleData()
    {
        WeaponId = 0;
        WeaponModel = "AssaultRifle";
        Damage = 15;
        AttackSpeed = 0.1f;
        ReloadTime = 2;
        AttackRange = 50;
        ProjectilesCount = 1;
        AmmoCount = 31;
        PenetrationPower = 0;
    }
}