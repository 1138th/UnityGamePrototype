using System.ComponentModel;
using UnityEngine;

public class LogicComponent : CharacterComponent, ILogicComponent
{
    private float timeBetweenAttack;

    public void ManualMove(Character target)
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // TODO: NEED to refactor/optimize that. DO NOT delete commented line
        if (target != null)
        {
            Character.MovableComponent.LookAt(target);
            
            if (Input.GetButtonDown("Jump")) Character.DamageComponent.DealDamage(target);
        }
        
        Character.MovableComponent.PlayerMove(movementVector);
        // Character.MovableComponent.LookAt(Input.mousePosition);
    }
    
    // TODO: For attacking using mouse as aim
    public void ManualAttack()
    {
    }
    
    public void AutoMove(Character target, ref AiState currentState)
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
