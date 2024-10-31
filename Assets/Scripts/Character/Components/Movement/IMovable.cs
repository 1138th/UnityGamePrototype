using UnityEngine;

public interface IMovable : ICharacterComponent
{
    public float Speed { get; set; }

    public void Move(Vector3 direction);

    public void LookAt(Character target);

    public void LookAt(Vector3 target);
}
