using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] public CharacterData data { get; private set; }

    public IMovable MovableComponent { get; protected set; }
    public ILogicComponent MovementLogicComponent { get; protected set; }
    public IHealthComponent HealthComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }

    public virtual void Start()
    {
        data = GetComponent<CharacterData>();

        MovableComponent = new CharacterMovementComponent();
        MovementLogicComponent = new MovementLogicComponent();

        MovableComponent.Initialize(this);
        MovementLogicComponent.Initialize(this);
    }

    public abstract void Update();
}
