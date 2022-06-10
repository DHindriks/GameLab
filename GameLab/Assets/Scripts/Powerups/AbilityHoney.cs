using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHoney : BasePowerup
{
    [SerializeField] GameObject AbilityPrefab;
    [SerializeField] float throwingSpeed;

    public override void Ability()
    {
        GameObject obj = Instantiate(AbilityPrefab);
        obj.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        if (obj.GetComponent<ActorTeam>())
        {
            obj.GetComponent<ActorTeam>().AssignTeam(GetComponentInParent<ActorTeam>().Team);
        }
        obj.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Sign(transform.parent.transform.lossyScale.x) * throwingSpeed, 0); //Adds speed to the throwable according to the players orientation

        Destroy(gameObject);
    }
}