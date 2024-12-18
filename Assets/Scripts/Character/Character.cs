using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterType type;

    public virtual Character Target { get; }
    public CharacterData Data { get; private set; }
    public CharacterType Type => type;

    public IMovable MovableComponent { get; protected set; }
    public ILogicComponent LogicComponent { get; protected set; }
    public IHealthComponent HealthComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }

    public virtual void Init()
    {
        Data = GetComponent<CharacterData>();

        MovableComponent = new CharacterMovementComponent();
        LogicComponent = new LogicComponent();

        MovableComponent.Initialize(this);
        LogicComponent.Initialize(this);
    }

    public abstract void Update();
}
