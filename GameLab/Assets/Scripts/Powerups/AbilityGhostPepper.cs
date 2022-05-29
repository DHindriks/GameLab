using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityGhostPepper : MonoBehaviour
{
    [SerializeField] private float duration;
    private bool pickedUp = false;
    InputAction Activate;
    UnityPlayerControls upc;

    // Update is called once per frame
    void Update()
    {
        if (pickedUp && Activate.IsPressed())
        {
            upc.isShielded = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pickedUp && collision.gameObject.tag == "Player")//If player touches it
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            transform.parent = collision.gameObject.transform;//Set position to player (now parent)
            transform.position = transform.parent.position;

            Activate = GetComponentInParent<PlayerInput>().actions["PowerUp"]; //Get the activate button
            pickedUp = true;

            upc = transform.parent.GetComponent<UnityPlayerControls>();
        }
    }
}
