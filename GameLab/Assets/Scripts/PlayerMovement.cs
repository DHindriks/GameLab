using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement vars
    Rigidbody2D rb;
    [SerializeField] float speed = 75;
    [SerializeField] float JumpForce = 5;
    [SerializeField] float Jumps = 1;
    float currentJumps;

    //orientation vars
    bool FacingRight = true;

    //ground checking vars
    bool grounded;
    [SerializeField] Transform GroundCheck;
    [SerializeField] float CheckRadius;
    [SerializeField] LayerMask GroundLayers;

    //crouch vars
    bool Crouching;
    BoxCollider2D coll;
    [SerializeField] float DefaultHeight;
    [SerializeField] float CrouchHeight;

    //animation vars
    [SerializeField] Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        currentJumps = Jumps;
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump")&& currentJumps > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            currentJumps--;
        }else if (Input.GetButtonDown("Jump")&& grounded)
        {
            currentJumps = Jumps;
            rb.velocity = Vector2.up * JumpForce;
            currentJumps--;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch(true);
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch(false);
        }

    }

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundLayers);

        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, rb.velocity.y);

            if (!FacingRight && Input.GetAxis("Horizontal") > 0)
            {
                Flip();
            }else if (FacingRight && Input.GetAxis("Horizontal") < 0)
            {
                Flip();
            }
        }
    }

    void crouch(bool crouched)
    {

        Crouching = crouched;
        animator.SetBool("Crouching", crouched);

        if (crouched)
        {
            coll.size = new Vector2(1, CrouchHeight);
            coll.offset = new Vector2(0, coll.size.y / 2);
        }
        else
        {
            coll.size = new Vector2(1, DefaultHeight);
            coll.offset = new Vector2(0, coll.size.y / 2);
        }
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
