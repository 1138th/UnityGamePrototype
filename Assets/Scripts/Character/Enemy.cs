using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private AiState currentState;

    public override Character Target => GameManager.Instance.CharacterFactory.Player;

    public override void Init()
    {
        base.Init();

        HealthComponent = gameObject.AddComponent<CharacterHealthComponent>();
        DamageComponent = gameObject.AddComponent<CharacterDamageComponent>();

        HealthComponent.Initialize(this);
        DamageComponent.Initialize(this);
    }

    public override void Update()
    {
        LogicComponent.EnemyMove(Target, ref currentState);
    }
}
