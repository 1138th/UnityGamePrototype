public class RevolverData : WeaponData
{
    // Base DPS (Per Target): 45
    // Base DPS (Per ALL Targets): 130
    public RevolverData()
    {
        WeaponId = 2;
        WeaponModel = "Revolver";
        Damage = 31.5f;
        AttackSpeed = 0.7f;
        ReloadTime = 2.5f;
        AttackRange = 150;
        ProjectilesCount = 1;
        AmmoCount = 6;
        PenetrationPower = 3;
    }
}