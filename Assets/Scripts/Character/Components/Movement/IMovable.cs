using UnityEngine;

public interface IMovable : ICharacterComponent
{
    public float Speed { get; set; }

    public void Move(Vector3 direction);
    
    public void Rotate(Vector3 direction);
}
