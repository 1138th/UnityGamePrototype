using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterType type;

    public virtual Character Target { get; }
    public CharacterData Data { get; protected set; }
    public CharacterType Type => type;

    public IMovable MovableComponent { get; protected set; }
    public IHealthComponent HealthComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }

    public virtual void Init()
    {
        Data = GetComponent<CharacterData>();

        MovableComponent = gameObject.AddComponent<CharacterMovementComponent>();

        MovableComponent.Initialize(this);
    }

    public abstract void Update();
}
