public class DroneLogicComponent : CharacterComponent, IDroneLogicComponent
{
    public void AttackEnemies(Character target)
    {
        GameManager.Instance.DroneShootingController.ShootBullets(Character);
    }
}