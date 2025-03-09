using UnityEngine;

public interface IMovable : ICharacterComponent
{
    public float Speed { get; set; }

    public void PlayerMove(Vector3 direction);
    
    public Vector3 EnemyMove(Character direction);

    public void LookAt(Character target);

    public void LookAt(Vector3 target);
}
