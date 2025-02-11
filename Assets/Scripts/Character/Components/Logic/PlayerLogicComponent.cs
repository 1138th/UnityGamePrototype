using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogicComponent : CharacterComponent, IPlayerLogicComponent
{
    private Vector3 playerMovementVector;
    private FloatingJoystick movementJoystick;
    private DynamicJoystick aimJoystick;
    private Button dashButton;
    
    private bool isAimManual = false;
    private float iFrameTime = 0.5f;
    private float iFrameExecutionTime = 0.5f;

    public new void Initialize(Character character)
    {
        base.Initialize(character);
        movementJoystick = FindObjectOfType<FloatingJoystick>();
        aimJoystick = FindObjectOfType<DynamicJoystick>();

        dashButton = GameObject.Find("Dash Button").GetComponent<Button>();
        dashButton.onClick.AddListener(ExecuteDash);
        
        playerMovementVector = Vector3.zero;
    }

    public void PlayerPcMove(Character target)
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        if (movementVector != Vector3.zero) playerMovementVector = movementVector;

        if (Input.GetButtonDown("Jump"))
        {
            isAimManual = !isAimManual;
        }
        if (isAimManual)
        {
            Character.MovableComponent.LookAt(Input.mousePosition);
            GameManager.Instance.PlayerShootingController.ShootBullets(Character);
        }
        else
        {
            if (target != null)
            {
                Character.MovableComponent.LookAt(target);
                GameManager.Instance.PlayerShootingController.ShootBullets(Character);
            }
        }
        Character.MovableComponent.PlayerMove(movementVector);
    }
    
    public void PlayerMobileMove(Character target)
    {
        var movementVector = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical).normalized;
        var aimVector = new Vector3(aimJoystick.Horizontal, 0, aimJoystick.Vertical).normalized;

        if (movementVector != Vector3.zero) playerMovementVector = movementVector;

        isAimManual = aimVector != Vector3.zero;

        if (isAimManual)
        {
            Character.transform.rotation = Quaternion.LookRotation(aimVector);
            GameManager.Instance.PlayerShootingController.ShootBullets(Character);
        }
        else
        {
            if (target != null)
            {
                Character.MovableComponent.LookAt(target);
                GameManager.Instance.PlayerShootingController.ShootBullets(Character);
            }
        }

        Character.MovableComponent.PlayerMove(movementVector);
    }

    public void PlayerDash()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            ExecuteDash();
        }
    }

    public void ExecuteDash()
    {
        Character.HealthComponent.MakeImmortal(true);
        StartCoroutine(CoroutineDash());
        while (iFrameExecutionTime >= 0)
        {
            iFrameExecutionTime -= Time.deltaTime;
        }
        Character.HealthComponent.MakeImmortal(false);
        iFrameExecutionTime = iFrameTime;
    }

    private IEnumerator CoroutineDash()
    {
        float startTime = Time.time;
        float targetAngle = Mathf.Atan2(playerMovementVector.x, playerMovementVector.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        while (Time.time < startTime + Character.Data.DashTime)
        {
            Character.Data.CharacterController.Move(
                move * (Character.Data.DashSpeed * UpgradesSystem.Instance.MoveSpeedAmp * Time.deltaTime));
            yield return null;
        }
    }
}
