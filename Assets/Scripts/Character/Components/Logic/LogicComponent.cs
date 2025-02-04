using System.ComponentModel;
using UnityEngine;

public class LogicComponent : CharacterComponent, ILogicComponent
{
    private float timeBetweenAttack;
    private bool isAimManual = false;

    public void PlayerMove(Character target)
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // if (Input.GetButtonDown("Fire3"))
        if (Input.GetButtonDown("Jump"))
        {
            isAimManual = !isAimManual;
        }
        if (isAimManual)
        {
            Character.MovableComponent.LookAt(Input.mousePosition);
            GameManager.Instance.ShootingController.ShootBullets();
        }
        else
        {
            if (target != null)
            {
                Character.MovableComponent.LookAt(target);
            
                GameManager.Instance.ShootingController.ShootBullets();
            }
        }
        Character.MovableComponent.PlayerMove(movementVector);
    }
    
    public void EnemyMove(Character target, ref AiState currentState)
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
                if (timeBetweenAttack <= 0)
                {
                    Character.DamageComponent.DealDamageToPlayer(target);
                    timeBetweenAttack = Character.Data.TimeBetweenAttacks;
                }
                else {
                    timeBetweenAttack -= Time.deltaTime;
                }
                break;
            case CharacterType.LongRangeSniperEnemy:
                Character.MovableComponent.LookAt(target);
                GameManager.Instance.ShootingController.ShootEnemyBullets(Character);
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character type: " + Character.Type);
        }
    }
}
