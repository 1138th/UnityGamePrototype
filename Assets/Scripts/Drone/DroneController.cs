using System.Collections.Generic;
using UnityEngine;

public class DroneController : Character
{
    private Character player;
    private CharacterController controller;
    private float speed;
    private float distanceToPlayer;

    public IDroneLogicComponent DroneLogicComponent;

    public override void Init()
    {
        base.Init();

        DamageComponent = gameObject.AddComponent<CharacterDamageComponent>();
        DroneLogicComponent = gameObject.AddComponent<DroneLogicComponent>();

        DamageComponent.Initialize(this);
        DroneLogicComponent.Initialize(this);

        player = GameManager.Instance.CharacterFactory.Player;
        speed = Data.DefaultSpeed;
        controller = Data.CharacterController;
        controller.detectCollisions = false;
        Physics.IgnoreLayerCollision(0, 6, true);

        transform.position = new Vector3(
            player.transform.position.x + 5.5f,
            player.transform.position.y + 1,
            player.transform.position.z - 3);
    }

    public override void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > 9)
        {
            transform.LookAt(new Vector3(
                player.transform.position.x,
                transform.position.y,
                player.transform.position.z));
            controller.Move(controller.transform.TransformDirection(Vector3.forward) * (speed * Time.deltaTime));
        }

        DroneLogicComponent.AttackEnemies(Target);
    }
}