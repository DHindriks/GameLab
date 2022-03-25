using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class NewPlayerMovement : NetworkBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float speed = 75;
    [SerializeField] float jumpForce = 5;
    [SerializeField] int jumps = 1;
    [SerializeField] int currentJumps;

    bool grounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask groundLayers;

    void Start()
    {    
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x / 4, rb.velocity.y);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (isLocalPlayer)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            rb.AddForce(new Vector2(inputVector.x, 0) * speed * Time.deltaTime, ForceMode2D.Force);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isLocalPlayer && context.started) 
        {
            resetJumps();
            if (currentJumps > 0 )
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                currentJumps--;
            }
        }
    }

    private void resetJumps()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayers))
        {
            currentJumps = jumps;
        }
    }
}
