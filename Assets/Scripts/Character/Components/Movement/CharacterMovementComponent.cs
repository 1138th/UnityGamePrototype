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
        if (direction == Vector3.zero) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        Character.Data.CharacterController.Move(move * (speed
                                                        * UpgradesSystem.MoveSpeedAmp
                                                        * EventManager.MoveSpeedAmp
                                                        * Time.deltaTime));
    }

    public Vector3 EnemyMove(Character target)
    {
        var controller = Character.Data.CharacterController;
        Vector3 motion = controller.transform.TransformDirection(Vector3.forward) * (speed * Time.deltaTime);
        LookAt(target);
        controller.Move(motion);
        return motion;
    }

    public void LookAt(Character target)
    {
        Character.transform.LookAt(target.transform);
    }

    public void LookAt(Vector3 target)
    {
        Ray cameraRay = cam.ScreenPointToRay(target);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out var rayLength))
        {
            Vector3 point = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, point, Color.red);

            Character.transform.LookAt(new Vector3(point.x, Character.transform.position.y, point.z));
        }
    }
}