using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHoney : MonoBehaviour
{
    [SerializeField] GameObject AbilityPrefab;
    [SerializeField] float throwingSpeed;

    private bool pickedUp = false;
    InputAction Activate;
    UnityPlayerControls upc;

    // Update is called once per frame
    void Update()
    {
        if (pickedUp && Activate.IsPressed())
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pickedUp && collision.gameObject.tag == "Player")//If player touches it
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.parent = collision.gameObject.transform;//Set position to player (now parent)
            transform.position = transform.parent.position;

            Activate = GetComponentInParent<PlayerInput>().actions["PowerUp"]; //Get the activate button
            pickedUp = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!pickedUp && collision.gameObject.tag == "Player")//If player touches it
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.parent = collision.gameObject.transform;//Set position to player (now parent)
            transform.position = transform.parent.position;

            Activate = GetComponentInParent<PlayerInput>().actions["PowerUp"]; //Get the activate button
            pickedUp = true;
        }
    }
}