using System;
using UnityEngine;

public class LogicComponent : CharacterComponent, ILogicComponent
{
    private float timeBetweenAttack;

    public void ManualMove()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        Character.MovableComponent.Move(movementVector);
        Character.MovableComponent.LookAt(Input.mousePosition);
    }
    
    public void AutoMove(Character target, ref AiState currentState)
    {
        var distanceToTarget = Vector3.Distance(target.transform.position, Character.transform.position);

        switch (currentState)
        {
            case AiState.MoveToTarget:
                var direction = (target.transform.position - Character.transform.position).normalized;

                Character.MovableComponent.Move(direction);
                Character.MovableComponent.LookAt(target);

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
                throw new ArgumentOutOfRangeException(nameof(currentState), currentState, "Invalid Character State!");
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
