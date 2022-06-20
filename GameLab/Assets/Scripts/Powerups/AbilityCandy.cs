using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCandy : BasePowerup
{
    [SerializeField] float duration;
    [SerializeField] float speedUp;
    float ogMaxSpeed;

    public override void Ability()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        ogMaxSpeed = base.upc.maxSpeed;
        base.upc.maxSpeed = speedUp;
        Invoke("ResetSpeed", duration);
    }

    void ResetSpeed()
    {
        base.upc.maxSpeed = ogMaxSpeed;
        Destroy(gameObject);
    }

}
