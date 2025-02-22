using System;
using UnityEngine;

public abstract class Laser : MonoBehaviour
{
    public event Action<Laser> Destroyed;

    protected Character Host;

    protected abstract void Update();

    public void SetHost(Character h)
    {
        Host = h;
    }

    protected void InvokeDestroyed(Laser l)
    {
        Destroyed?.Invoke(l);
    }
}