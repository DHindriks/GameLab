using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityRollingpin : AbilityBase
{
    [SerializeField] GameObject AbilityPrefab;
    [SerializeField] float throwingSpeed;
    InputAction Activate;

    void Start()
    {
        Activate = GetComponentInParent<PlayerInput>().actions["PowerUp"];
    }

    // Update is called once per frame
    void Update()
    {
        if (Activate.IsPressed())
        {
            GameObject obj = Instantiate(AbilityPrefab);
            obj.transform.position = transform.GetChild(0).position;
            //obj.GetComponent<Rigidbody2D>().AddForce(transform.forward * 1000);
            if (obj.GetComponent<ActorTeam>())
            {
                obj.GetComponent<ActorTeam>().AssignTeam(GetComponentInParent<ActorTeam>().Team);
            }

            obj.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Sign(transform.parent.transform.lossyScale.x) * throwingSpeed, 0); //Adds speed to the throwable according to the players orientation
            Destroy(gameObject);
        }
    }
}
