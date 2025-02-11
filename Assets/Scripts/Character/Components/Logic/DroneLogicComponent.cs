using UnityEngine;

public class DroneLogicComponent : CharacterComponent, IDroneLogicComponent
{
    public void AttackEnemies(Character target)
    {
        if (target != null)
        {
            var distanceToTarget = Vector3.Distance(target.transform.position, Character.transform.position);
            if (distanceToTarget <= Character.Data.AttackRange)
            {
                // GameManager.Instance.ShootingController.ShootBullets();
            }
        }
    }
}