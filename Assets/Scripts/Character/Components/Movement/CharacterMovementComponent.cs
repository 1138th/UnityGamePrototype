using UnityEngine;

public class CharacterMovementComponent : CharacterComponent, IMovable
{
    private float speed;
    private Camera camera;

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
        camera = Camera.main;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        Character.data.CharacterController.Move(move * speed * Time.deltaTime);
    }

    public void LookAt(Character target)
    {
        Character.transform.LookAt(target.transform.position);
    }

    public void LookAt(Vector3 target)
    {
        var cameraRay = camera.ScreenPointToRay(target);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out var rayLength))
        {
            var point = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, point, Color.red);

            Character.transform.LookAt(new Vector3(point.x, Character.transform.position.y, point.z));
        }
    }
}