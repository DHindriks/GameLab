using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCandy : BasePowerup
{
    [SerializeField] float duration;
    [SerializeField] float speedUp;

    public override void Ability()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        base.upc.maxSpeed = speedUp;
        Invoke("ResetSpeed", duration);
    }

    void ResetSpeed()
    {
        base.upc.maxSpeed = base.upc.OgmaxSpeed;
        Destroy(gameObject);
    }

}
