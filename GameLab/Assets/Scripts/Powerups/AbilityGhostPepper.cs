using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityGhostPepper : MonoBehaviour
{
    [SerializeField] private float duration;
    private bool pickedUp = false;
    private bool used = false;
    InputAction Activate;

    // Update is called once per frame
    void Update()
    {
        if (pickedUp && Activate.IsPressed())
        {
            used = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = false;
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

            BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();//Set the collider slightly bigger than the player
            collider.offset = GetComponentInParent<BoxCollider2D>().offset;
            collider.size = new Vector2(GetComponentInParent<BoxCollider2D>().size.x, GetComponentInParent<BoxCollider2D>().size.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used && collision.gameObject.tag == "Rolling Pin")
        {
            Debug.Log("sghdv");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
