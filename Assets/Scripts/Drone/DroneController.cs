using System.Collections.Generic;
using UnityEngine;

public class DroneController : Character
{
    private Character player;
    private CharacterController controller;
    private float speed;
    private float distanceToPlayer;

    public IDroneLogicComponent DroneLogicComponent;

    public override Character Target
    {
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> characters = GameManager.Instance.CharacterFactory.ActiveCharacters;
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].Type == CharacterType.Player) continue;
                float distance = Vector3.Distance(characters[i].transform.position, transform.position);
                if (distance < minDistance)
                {
                    target = characters[i];
                    minDistance = distance;
                }
            }
            return target;
        }
    }

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

        transform.position = new Vector3(
            player.transform.position.x + 5.5f,
            player.transform.position.y + 12.5f,
            player.transform.position.z - 3);
    }

    public override void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > 15)
        {
            transform.LookAt(new Vector3(
                player.transform.position.x + 5.5f,
                transform.position.y,
                player.transform.position.z - 3));
            controller.Move(controller.transform.TransformDirection(Vector3.forward) * (speed * Time.deltaTime));
        }

        DroneLogicComponent.AttackEnemies(Target);
    }
}