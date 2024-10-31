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
        HealthComponent.Initialize(this);
        DamageComponent.Initialize(this);
    }

    public override void Update()
    {
        var distanceToTarget = Vector3.Distance(targetCharacter.transform.position, transform.position);

        switch (currentState)
        {
            case AiState.MoveToTarget:
                Vector3 direction = targetCharacter.transform.position - transform.position;
                direction.Normalize();

                MovableComponent.Move(direction);
                MovableComponent.Rotate(direction);

                if (distanceToTarget < characterData.AttackRange)
                {
                    currentState = AiState.Attack;
                }
                break;
            case AiState.Attack:
                if (distanceToTarget >= characterData.AttackRange)
                {
                    currentState = AiState.MoveToTarget;
                }
                if (timeBetweenAttack <= 0)
                {
                    DamageComponent.DoDamage(targetCharacter);
                    timeBetweenAttack = characterData.TimeBetweenAttacks;
                }
                else {
                    timeBetweenAttack -= Time.deltaTime;
                }
                break;
        }
    }
}
