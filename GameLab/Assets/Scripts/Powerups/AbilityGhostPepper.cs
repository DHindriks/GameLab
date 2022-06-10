using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityGhostPepper : BasePowerup
{
    [SerializeField] private float duration;

    public override void Ability()
    {
        base.upc.isShielded = true;
        Destroy(gameObject);
    }
}
