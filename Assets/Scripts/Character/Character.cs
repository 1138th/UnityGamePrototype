using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] public CharacterData characterData { get; private set; }

    public IMovable MovableComponent { get; protected set; }
    public IHealthComponent HealthComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }

    public virtual void Start()
    {
        characterData = GetComponent<CharacterData>();
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(this);
    }

    public abstract void Update();
}
