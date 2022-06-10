using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePowerup : MonoBehaviour
{
    private bool pickedUp = false;
    InputAction Activate;
    public UnityPlayerControls upc;

    private void Update()
    {
        if (pickedUp && Activate.IsPressed())
        {
            pickedUp = false;
            upc.hasPowerup = false;
            Ability();
        }
    }

    public virtual void Ability()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpItem(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PickUpItem(collision);
    }

    void PickUpItem(Collider2D collision)
    {
        if (!pickedUp && collision.gameObject.tag == "Player" && collision)//If player touches it
        {
            upc = collision.gameObject.GetComponent<UnityPlayerControls>();

            if (!upc.hasPowerup)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                transform.parent = collision.gameObject.transform;//Set position to player (now parent)
                transform.position = transform.parent.position;

                Activate = GetComponentInParent<PlayerInput>().actions["PowerUp"]; //Get the activate button
                pickedUp = true;
                upc.hasPowerup = true;
            }
        }
    }
}
