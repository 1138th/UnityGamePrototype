using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private AiState currentState;

    public override Character Target => GameManager.Instance.CharacterFactory.Player;

    public IEnemyLogicComponent LogicComponent;

    public override void Init()
    {
        if (LogicComponent == null)
        {
            base.Init();

            HealthComponent = gameObject.AddComponent<CharacterHealthComponent>();
            DamageComponent = gameObject.AddComponent<CharacterDamageComponent>();
            LogicComponent = gameObject.AddComponent<EnemyLogicComponent>();
        }

        HealthComponent.Initialize(this);
        DamageComponent.Initialize(this);
        LogicComponent.Initialize(this);
    }

    public override void Update()
    {
        LogicComponent.EnemyMove(Target, ref currentState);
    }
}
