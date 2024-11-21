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
        if (timeBetweenAttack <= 0)
        {
            Character.DamageComponent.DealDamage(target);
            timeBetweenAttack = Character.Data.TimeBetweenAttacks;
        }
        else {
            timeBetweenAttack -= Time.deltaTime;
        }
    }
}
