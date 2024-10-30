using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Character targetCharacter;
    [SerializeField] private AiState currentState;
    private float timeBetweenAttack = 0;

    public override void Start()
    {
        base.Start();

        HealthComponent = new ImmortalComponent();
        DamageComponent = new CharacterDamageComponent();
    }

    public override void Update()
    {
        switch (currentState)
        {
            case AiState.None:
                break;
            case AiState.MoveToTarget:
                Vector3 direction = targetCharacter.transform.position - transform.position;
                direction.Normalize();

                MovableComponent.Move(direction);
                MovableComponent.Rotate(direction);

                if (Vector3.Distance(targetCharacter.transform.position, transform.position) < 3 && timeBetweenAttack <= 0)
                {
                    DamageComponent.DoDamage(targetCharacter);
                    timeBetweenAttack = characterData.TimeBetweenAttacks;
                }
                if (timeBetweenAttack > 0)
                {
                    timeBetweenAttack -= Time.deltaTime;
                }
                break;
        }
    }
}
