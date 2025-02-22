using System.ComponentModel;
using UnityEngine;

public class EnemyLogicComponent : CharacterComponent, IEnemyLogicComponent
{
    protected float TimeBetweenAttack;

    protected const float MaxDistanceOffset = 70;

    public virtual void EnemyMove(Character target, ref AiState currentState)
    {
        var distanceToTarget = Vector3.Distance(target.transform.position, Character.transform.position);

        switch (currentState)
        {
            case AiState.MoveToTarget:
                Character.MovableComponent.EnemyMove(target);

                if (distanceToTarget < Character.Data.AttackRange)
                {
                    currentState = AiState.Attack;
                }

                if (distanceToTarget >= MaxDistanceOffset)
                {
                    RespawnFarEnemy(target.transform.position);
                }
                break;
            case AiState.Attack:
                if (distanceToTarget >= Character.Data.AttackRange)
                {
                    currentState = AiState.MoveToTarget;
                }
                Attack(target);
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character state: " + currentState);
        }
    }

    private void Attack(Character target)
    {
        switch (Character.Type)
        {
            case CharacterType.DefaultEnemy:
                if (TimeBetweenAttack <= 0)
                {
                    Character.DamageComponent.DealDamageToPlayer(target);
                    TimeBetweenAttack = Character.Data.TimeBetweenAttacks;
                }
                else {
                    TimeBetweenAttack -= Time.deltaTime;
                }
                break;
            case CharacterType.LongRangeSniperEnemy:
                Character.MovableComponent.LookAt(target);
                GameManager.Instance.EnemyShootingController.ShootBullets(Character);
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character type: " + Character.Type);
        }
    }

    /// <summary>
    /// Respawns enemies on the opposite side of a circle around the player 
    /// </summary>
    protected void RespawnFarEnemy(Vector3 playerPosition)
    {
        Vector3 temp = playerPosition - Character.transform.position;

        Character.transform.position = temp.normalized * (MaxDistanceOffset - 10) + playerPosition;
    }
}