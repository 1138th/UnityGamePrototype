using System;
using UnityEngine;

public class MovementLogicComponent : CharacterComponent, ILogicComponent
{
    private float timeBetweenAttack = 0;

    public void ManualMove()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        Character.MovableComponent.Move(movementVector);
        Character.MovableComponent.Rotate(movementVector);
    }
    
    public void AutoMove(Character targetCharacter, ref AiState currentState)
    {
        var distanceToTarget = Vector3.Distance(targetCharacter.transform.position, Character.transform.position);

        switch (currentState)
        {
            case AiState.MoveToTarget:
                var direction = targetCharacter.transform.position - Character.transform.position;
                direction.Normalize();

                Character.MovableComponent.Move(direction);
                Character.MovableComponent.Rotate(direction);

                if (distanceToTarget < Character.data.AttackRange)
                {
                    currentState = AiState.Attack;
                }
                break;
            case AiState.Attack:
                if (distanceToTarget >= Character.data.AttackRange)
                {
                    currentState = AiState.MoveToTarget;
                }
                if (timeBetweenAttack <= 0)
                {
                    Character.DamageComponent.DealDamage(targetCharacter);
                    timeBetweenAttack = Character.data.TimeBetweenAttacks;
                }
                else {
                    timeBetweenAttack -= Time.deltaTime;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(currentState), currentState, "Invalid Character State!");
        }
    }
}
