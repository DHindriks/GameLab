using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UnityPlayerControls : MonoBehaviour
{
    //Input
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction duckAction;
    Vector2 move;
    float jump;

    private Rigidbody2D rb;

    //Scoring
    public int Coins;
    [SerializeField] TextMeshProUGUI CoinCounter;

    //Player gameover
    private int RespawnTimer;
    Vector2 RespawnPoint;
    [SerializeField] GameObject CoinPrefab;


    //TempBullshit for testing
    [SerializeField] Sprite spr1;
    [SerializeField] Sprite spr2;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        RespawnPoint = transform.position;
        mapControls();
        assignSprites();
    }

    private void FixedUpdate()
    {
        GatherInput();
        CalculateMovement();
        CalculateJump();
        CommitMovement();
    }

    public void AddCoin(int amount = 1)
    {
        Coins += amount;
        CoinCounter.text = Coins.ToString();
    }

    public void KillPlayer()
    {
        //for(int i = 0; i < Coins; i++)
        //{
        //    GameObject Coin = Instantiate(CoinPrefab);
        //}

        transform.position = RespawnPoint;
        Coins = 0;
    }

    void GatherInput()
    {
        move = moveAction.ReadValue<Vector2>();
        jump = jumpAction.ReadValue<float>();
    }

    #region Movement
    [Header("Movement")] 
    [SerializeField] private float speed;
    [SerializeField] [Range(0, 1)] private float deadzone;
    private float _horizontalSpeed;

        void CalculateMovement()
        {
            if (Mathf.Abs(move.x) >= deadzone)
            {
                _horizontalSpeed = move.x * speed * Time.deltaTime;
            } else
            {
                _horizontalSpeed = 0;
            }
        }
    #endregion

    #region Jump
    [Header("Jump")]
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private float jumpSpeed;
    [SerializeField][Range(0, 0.5f)] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private float jumpApexThreshold = 0.25f;
    [SerializeField] private float jumpApexThresholdStep = 0.05f;
    [SerializeField] private float gravity;
    [SerializeField] private float minFallSpeed = 40f;
    [SerializeField] private float maxFallSpeed = 120f;
    private int numberOfJumps;
    private float _verticalSpeed;
    private bool canJump = true;
    private bool grounded = true;

    void CalculateJump()
    {
        _verticalSpeed = float.NaN;
        if (Physics2D.BoxCast(transform.position, new Vector2(GetComponent<BoxCollider2D>().size.x, groundCheckDistance), 0, Vector2.down, 0f, GroundMask))
        {
            grounded = true;
            numberOfJumps = maxJumps;
        }
        else
        {
            grounded = false;
            numberOfJumps--;
        }
        //Keep the falling speed of the player within the boundaries
        if (rb.velocity.y <= 0)
        {
            if (rb.velocity.y > -minFallSpeed)
            {
                _verticalSpeed = -minFallSpeed;
            }
            else if (rb.velocity.y < -maxFallSpeed)
            {
                _verticalSpeed = -maxFallSpeed;
            }
        }
        if (jump == 1 && numberOfJumps > 0 && canJump)
        {
            _verticalSpeed = jumpSpeed;
            numberOfJumps--;
            canJump = false;
        }
        else
        {
            //Decrease the gravity affecting this object while the player holds down jump
            if (jump == 1 && rb.velocity.y > 0 && rb.gravityScale >= jumpApexThreshold)
            {
                rb.gravityScale -= jumpApexThresholdStep * Time.deltaTime;
            }
            else if (jump == 0)
            {
                canJump = true;
                rb.gravityScale = gravity;
            }
        }
    }
    #endregion

    void CommitMovement()
    {
        if (!float.IsNaN(_verticalSpeed))
        {
            rb.velocity = new Vector2(_horizontalSpeed, _verticalSpeed);
        } 
        else
        {
            Vector2 vel = rb.velocity;
            vel.x = _horizontalSpeed;
            rb.velocity = vel;
        }
    }

    //For the current multiplayer options
    #region Automatic sprite assign
    void assignSprites()
        {
            if (playerInput.playerIndex == 1)
            {
                GetComponent<SpriteRenderer>().sprite = spr1;
            }
            else if (playerInput.playerIndex == 2)
            {
                GetComponent<SpriteRenderer>().sprite = spr2;
            }
        }
    #endregion

    #region Map the buttons
        private void mapControls()
        {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
            duckAction = playerInput.actions["Duck"];
        }
    #endregion
}