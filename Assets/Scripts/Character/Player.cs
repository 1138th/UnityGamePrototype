using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public override void Start()
    {
        base.Start();

        HealthComponent = new CharacterHealthComponent();
    }

    public override void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        MovableComponent.Move(movementVector);
        MovableComponent.Rotate(movementVector);
    }
}
