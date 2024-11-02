public class Player : Character
{
    public override void Start()
    {
        base.Start();

        HealthComponent = new CharacterHealthComponent();
        HealthComponent.Initialize(this);
    }

    public override void Update()
    {
        LogicComponent.ManualMove();
    }
}