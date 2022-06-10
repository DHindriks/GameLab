using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityRollingpin : BasePowerup
{
    [SerializeField] GameObject AbilityPrefab;
    [SerializeField] float throwingSpeed;

    public override void Ability()
    {
        GameObject obj = Instantiate(AbilityPrefab);
        obj.transform.position = transform.GetChild(0).position;
        if (obj.GetComponent<ActorTeam>())
        {
            obj.GetComponent<ActorTeam>().AssignTeam(GetComponentInParent<ActorTeam>().Team);
        }

        obj.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Sign(transform.parent.transform.lossyScale.x) * throwingSpeed, 0); //Adds speed to the throwable according to the players orientation
        Destroy(gameObject);
    }
}
