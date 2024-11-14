using UnityEngine;

public class CharacterMovementComponent : CharacterComponent, IMovable
{
    private float speed;
    private Camera cam;

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
        speed = Character.Data.DefaultSpeed;
        cam = Camera.main;
    }

    public void PlayerMove(Vector3 direction)
    {
        // TODO: simple alternative. Most likely will be used in the future, so PlayerMove and EnemyMove can be merged
        // Character.transform.position += direction * speed * Time.deltaTime;
        if (direction == Vector3.zero) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        Character.Data.CharacterController.Move(move * (speed * Time.deltaTime));
    }

    public void EnemyMove(Character target)
    {
        LookAt(target);
        Character.transform.position += Character.transform.forward * (speed * Time.deltaTime);
    }

    public void LookAt(Character target)
    {
        Character.transform.LookAt(target.transform);
    }

    public void LookAt(Vector3 target)
    {
        var cameraRay = cam.ScreenPointToRay(target);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out var rayLength))
        {
            var point = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, point, Color.red);

            Character.transform.LookAt(new Vector3(point.x, Character.transform.position.y, point.z));
        }
    }
}