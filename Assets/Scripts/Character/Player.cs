using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

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

        HealthComponent = new CharacterHealthComponent();
        DamageComponent = new CharacterDamageComponent();
        
        HealthComponent.Initialize(this);
        DamageComponent.Initialize(this);

        Data.HealthBar.maxValue = Data.MaxHealth;
        Data.HealthBar.value = Data.MaxHealth;
    }

    public override void Update()
    {
        LogicComponent.PlayerMove(Target);
    }
}