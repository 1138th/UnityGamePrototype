using System.ComponentModel;
using UnityEngine;

public class LaserEnemyLogicComponent : EnemyLogicComponent
{
    private float reactTime;

    public override void EnemyMove(Character target, ref AiState currentState)
    {
        var distanceToTarget = Vector3.Distance(target.transform.position, Character.transform.position);
    
        switch (currentState)
        {
            case AiState.None:
                if (distanceToTarget >= MaxDistanceOffset)
                {
                    Character.HealthComponent.ExecuteDeath();
                }
                break;
            case AiState.MoveToTarget:
                Character.MovableComponent.EnemyMove(target);
    
                if (distanceToTarget < Character.Data.AttackRange)
                {
                    currentState = AiState.PrepareForAttack;
                }
    
                if (distanceToTarget >= MaxDistanceOffset)
                {
                    RespawnFarEnemy(target.transform.position);
                }
    
                break;
            case AiState.PrepareForAttack:
                GameManager.Instance.EnemyShootingController.PrepareLaser(Character);
                currentState = AiState.Aiming;
    
                TimeBetweenAttack = Character.Data.TimeBetweenAttacks;
                reactTime = Character.Data.AttackDelay;
    
                break;
            case AiState.Aiming:
                Character.MovableComponent.LookAt(target);
    
                if (TimeBetweenAttack <= 0)
                {
                    currentState = AiState.Attack;
    
                    TimeBetweenAttack = Character.Data.TimeBetweenAttacks;
                }
                else
                {
                    TimeBetweenAttack -= Time.deltaTime;
                }
    
                break;
            case AiState.Attack:
                if (reactTime <= 0)
                {
                    GameManager.Instance.EnemyShootingController.ShootLaser(Character);
                    currentState = AiState.None;
    
                    reactTime = Character.Data.AttackDelay;
                }
                else
                {
                    reactTime -= Time.deltaTime;
                }
    
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character state: " + currentState);
        }
    }
}