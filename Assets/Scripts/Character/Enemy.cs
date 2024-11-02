using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Character targetCharacter;
    [SerializeField] private AiState currentState;

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
        LogicComponent.AutoMove(targetCharacter, ref currentState);
    }
}
