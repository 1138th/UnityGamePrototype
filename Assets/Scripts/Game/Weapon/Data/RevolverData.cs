public class RevolverData : WeaponData
{
    // Base DPS (Per Target): 45
    // Base DPS (Per ALL Targets): 130
    public RevolverData()
    {
        WeaponId = 2;
        WeaponModel = "Revolver";
        Damage = 31.5f;
        AttackSpeed = 0.3f;
        ReloadTime = 1.5f;
        AttackRange = 50;
        ProjectilesCount = 1;
        AmmoCount = 6;
        PenetrationPower = 3;
    }
}