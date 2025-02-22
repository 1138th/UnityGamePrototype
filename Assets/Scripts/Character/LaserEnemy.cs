public class LaserEnemy : Enemy
{
    public override void Init()
    {
        Data = GetComponent<CharacterData>();

        if (LogicComponent == null)
        {
            HealthComponent = gameObject.AddComponent<CharacterHealthComponent>();
            DamageComponent = gameObject.AddComponent<CharacterDamageComponent>();
            LogicComponent = gameObject.AddComponent<LaserEnemyLogicComponent>();
            MovableComponent = gameObject.AddComponent<CharacterMovementComponent>();
        }

        HealthComponent.Initialize(this);
        DamageComponent.Initialize(this);
        LogicComponent.Initialize(this);
        MovableComponent.Initialize(this);

        currentState = AiState.MoveToTarget;
    }
}