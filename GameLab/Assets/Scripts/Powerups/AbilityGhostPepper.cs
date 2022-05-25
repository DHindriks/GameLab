using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityGhostPepper : MonoBehaviour
{
    [SerializeField] private float duration;
    private bool pickedUp = false;
    [HideInInspector] public bool used = false;
    InputAction Activate;

    SpriteRenderer sprRender;
    BoxCollider2D boxCollider;

    private void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp && Activate.IsPressed())
        {
            used = true;
            boxCollider.enabled = true;
            sprRender.enabled = false;
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
        }
    }
}
