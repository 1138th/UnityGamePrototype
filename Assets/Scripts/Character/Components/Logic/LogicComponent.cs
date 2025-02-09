using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class LogicComponent : CharacterComponent, ILogicComponent
{
    private FloatingJoystick movementJoystick;
    private DynamicJoystick aimJoystick;
    
    private float timeBetweenAttack;
    private bool isAimManual = false;

    public new void Initialize(Character character)
    {
        base.Initialize(character);
        movementJoystick = Object.FindObjectOfType<FloatingJoystick>();
        aimJoystick = Object.FindObjectOfType<DynamicJoystick>();
    }

    public void PlayerPcMove(Character target)
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (Input.GetButtonDown("Jump"))
        {
            isAimManual = !isAimManual;
        }
        if (isAimManual)
        {
            Character.MovableComponent.LookAt(Input.mousePosition);
            GameManager.Instance.ShootingController.ShootBullets();
        }
        else
        {
            if (target != null)
            {
                Character.MovableComponent.LookAt(target);
                GameManager.Instance.ShootingController.ShootBullets();
            }
        }
        Character.MovableComponent.PlayerMove(movementVector);
    }
    
    public void PlayerMobileMove(Character target)
    {
        var movementVector = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical).normalized;
        var aimVector = new Vector3(aimJoystick.Horizontal, 0, aimJoystick.Vertical).normalized;

        isAimManual = aimVector != Vector3.zero;

        if (isAimManual)
        {
            Character.transform.rotation = Quaternion.LookRotation(aimVector);
            GameManager.Instance.ShootingController.ShootBullets();
        }
        else
        {
            if (target != null)
            {
                Character.MovableComponent.LookAt(target);
                GameManager.Instance.ShootingController.ShootBullets();
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
        if (Character == null) Character = GameManager.Instance.CharacterFactory.Player;
        StartCoroutine(CoroutineDash());
    }

    private IEnumerator CoroutineDash()
    {
        float startTime = Time.time;
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        while (Time.time < startTime + Character.Data.DashTime)
        {
            Character.Data.CharacterController.Move(
                move * (Character.Data.DashSpeed * UpgradesSystem.Instance.MoveSpeedAmp * Time.deltaTime));
            yield return null;
        }
    }

    public void EnemyMove(Character target, ref AiState currentState)
    {
        var distanceToTarget = Vector3.Distance(target.transform.position, Character.transform.position);

        switch (currentState)
        {
            case AiState.MoveToTarget:
                Character.MovableComponent.EnemyMove(target);

                if (distanceToTarget < Character.Data.AttackRange)
                {
                    currentState = AiState.Attack;
                }
                break;
            case AiState.Attack:
                if (distanceToTarget >= Character.Data.AttackRange)
                {
                    currentState = AiState.MoveToTarget;
                }
                Attack(target);
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character state: " + currentState);
        }
    }

    private void Attack(Character target)
    {
        switch (Character.Type)
        {
            case CharacterType.DefaultEnemy:
                if (timeBetweenAttack <= 0)
                {
                    Character.DamageComponent.DealDamageToPlayer(target);
                    timeBetweenAttack = Character.Data.TimeBetweenAttacks;
                }
                else {
                    timeBetweenAttack -= Time.deltaTime;
                }
                break;
            case CharacterType.LongRangeSniperEnemy:
                Character.MovableComponent.LookAt(target);
                GameManager.Instance.ShootingController.ShootEnemyBullets(Character);
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid character type: " + Character.Type);
        }
    }
}
