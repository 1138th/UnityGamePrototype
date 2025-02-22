using UnityEngine;

public interface IPlayerLogicComponent : ILogicComponent
{
    public Vector3 PlayerPcMove(Character target);

    public Vector3 PlayerMobileMove(Character target);

    public void PlayerDash();
}