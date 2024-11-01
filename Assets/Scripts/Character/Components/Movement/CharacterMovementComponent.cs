using UnityEngine;

public class CharacterMovementComponent : CharacterComponent, IMovable
{
    private float speed;

    public float Speed
    {
        get => speed;
        set
        {
            if (value < 0)
                return;
            speed = value;
        }
    }

    public new void Initialize(Character character)
    {
        base.Initialize(character);
        speed = Character.data.DefaultSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        Character.data.CharacterController.Move(move * speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        var rotationSmoothness = 0.1f;
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(Character.data.CharacterTransform.eulerAngles.y, targetAngle, ref rotationSmoothness, rotationSmoothness);
    }
}
