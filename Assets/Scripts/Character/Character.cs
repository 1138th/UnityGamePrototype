using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public CharacterData Data { get; private set; }

    public IMovable MovableComponent { get; protected set; }
    public ILogicComponent LogicComponent { get; protected set; }
    public IHealthComponent HealthComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }

    public virtual void Start()
    {
        Data = GetComponent<CharacterData>();

        MovableComponent = new CharacterMovementComponent();
        LogicComponent = new LogicComponent();

        MovableComponent.Initialize(this);
        LogicComponent.Initialize(this);
    }

    public abstract void Update();
}
