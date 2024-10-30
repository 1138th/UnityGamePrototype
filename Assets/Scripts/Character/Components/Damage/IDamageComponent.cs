using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageComponent
{
    public float Damage { get; }

    public void DoDamage(Character target);
}
