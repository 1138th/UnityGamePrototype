using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public IPlayerLogicComponent LogicComponent;

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

        HealthComponent = gameObject.AddComponent<CharacterHealthComponent>();
        DamageComponent = gameObject.AddComponent<CharacterDamageComponent>();
        LogicComponent = gameObject.AddComponent<PlayerLogicComponent>();

        HealthComponent.Initialize(this);
        DamageComponent.Initialize(this);
        LogicComponent.Initialize(this);

        Data.HealthBar.maxValue = Data.MaxHealth;
        Data.HealthBar.value = Data.MaxHealth;
    }

    public override void Update()
    {
        Vector3 movementVector = LogicComponent.PlayerPcMove(Target);
        // Vector3 movementVector = LogicComponent.PlayerMobileMove(Target);
        movementVector = Quaternion.Euler(0, 360 - transform.rotation.eulerAngles.y, 0) * movementVector;

        Animator.SetFloat("Horizontal", movementVector.x);
        Animator.SetFloat("Vertical", movementVector.z);
        Animator.SetFloat("Magnitude", movementVector.magnitude);

        LogicComponent.PlayerDash();
        HealthComponent.RegenerateHealth();
    }
}