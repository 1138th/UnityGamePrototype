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
        Character = character;
        speed = Character.characterData.DefaultSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        Character.characterData.CharacterController.Move(move * speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        float rotationSmoothness = 0.1f;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(Character.characterData.CharacterTransform.eulerAngles.y, targetAngle, ref rotationSmoothness, rotationSmoothness);
    }
}
