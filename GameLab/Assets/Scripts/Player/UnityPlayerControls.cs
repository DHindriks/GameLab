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
    public InputAction useAction;
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
    public bool isShielded = false;
    public bool isInvincilbe = false;
    public bool isHoneyed = false;
    public float honeyedTimer;
    [SerializeField][Range(0, 10)] float afterHitInvincibilityTime;
    float invincibilityTimer;

    //Sprite
    SpriteRenderer sprRender;

    //TempBullshit for testing
    [SerializeField] Sprite spr1;
    [SerializeField] Sprite spr2;

    void Awake()
    {
        sprRender = GetComponent<SpriteRenderer>();
        GameObject.DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        RespawnPoint = new Vector2(0, 2);
        invincibilityTimer = afterHitInvincibilityTime;

        mapControls();
        assignSprites();
    }

    private void Update()
    {
        ChangeOrientation();
        AfterHitInvincibility();
        decayHoney();
        changeSprites();
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

    public void SetCharacter(GameObject CharObj)
    {
        if (CharObj == null)
        {
            return;
        }

        foreach(Transform transform in transform.GetChild(1))
        {
            Destroy(transform.gameObject);
        }
        GameObject NewSkin = Instantiate(CharObj, transform.GetChild(1));
        NewSkin.transform.localPosition = Vector3.zero;

    }

    void GatherInput()
    {
        move = moveAction.ReadValue<Vector2>();
        jump = jumpAction.ReadValue<float>();

        if (isHoneyed)
        {
            move.x = 0;
        }
    }

    #region Movement
    [Header("Movement")]
    [SerializeField] private float maxSpeed;
    //[SerializeField] [Range(0, 1)] private float innerDeadzone; //This is stupid to handle it here
    //[SerializeField] [Range(0, 1)] private float outerDeadzone;

    [Header("Acceleration")]
    [SerializeField] private bool doAccelerate = false;
    [SerializeField][Range(0, 1.5f)] private float accelerationRate;
    [SerializeField][Range(0, 1)] private float accelerationOffset;
    [SerializeField] private bool doMinSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private bool doDecelerate = false;
    [SerializeField] private float decelerationRate;
    private float _modifier;

    private float _horizontalSpeed;

    void CalculateMovement()
    {
        if (_horizontalSpeed == 0 || move.x == 0 || Mathf.Sign(_horizontalSpeed) != Mathf.Sign(move.x))
        {
            _modifier = accelerationOffset;
        }

        if (move.x != 0)
        {
            if (doAccelerate)
            {
                _modifier -= accelerationRate * Time.deltaTime;//Changing the acceleration modifier

                if (_modifier < 0)
                {
                    _modifier = 0;
                }

                #region Has minimum starting speed
                if (doMinSpeed && (Mathf.Abs(_horizontalSpeed) < minSpeed + Mathf.Abs(move.x - (move.x * accelerationOffset)) || Mathf.Sign(_horizontalSpeed) != Mathf.Sign(move.x)))//Setting a minimum start speed if enabled
                {
                    _horizontalSpeed = minSpeed * Mathf.Sign(move.x) + (move.x - (move.x * accelerationOffset));
                } else
                {
                    _horizontalSpeed = (move.x - (move.x * _modifier)) * maxSpeed; //Setting the speed
                }
                #endregion

                #region There is no minimum starting speed
                if (!doMinSpeed && Mathf.Sign(_horizontalSpeed) != Mathf.Sign(move.x)) //Resetting the speed when changing directions to prevent sliding
                {
                    _horizontalSpeed = 0;
                }

                if (!doMinSpeed)
                {
                    _horizontalSpeed = (move.x - (move.x * _modifier)) * maxSpeed; //Setting the speed
                }
                #endregion

                if (Mathf.Abs(_horizontalSpeed) > maxSpeed)//Capping max speed
                {
                    _horizontalSpeed = maxSpeed * Mathf.Sign(_horizontalSpeed);
                }
            }
            else
            {
                _horizontalSpeed = move.x * maxSpeed;
            }
        }
        else
        {
            if (doDecelerate)
            {
                if (_horizontalSpeed > 0)
                {
                    _horizontalSpeed -= decelerationRate * Time.deltaTime;
                    if (_horizontalSpeed < 0)
                    {
                        _horizontalSpeed = 0;
                    }
                } 
                else if (_horizontalSpeed < 0)
                {
                    _horizontalSpeed += decelerationRate * Time.deltaTime;
                    if (_horizontalSpeed > 0)
                    {
                        _horizontalSpeed = 0;
                    }
                }
            }
            else
            {
                _horizontalSpeed = 0;
            }
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

    [Header("Gravity")]
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
        if (Physics2D.BoxCast(transform.position, new Vector2(GetComponent<BoxCollider2D>().size.x * 0.8f, groundCheckDistance), 0, Vector2.down, 0f, GroundMask)) //the horixontal size must be lowered from the actual size so that vertical walls wont be counted as being on the ground
        {
            grounded = true;
            numberOfJumps = maxJumps;
        }
        else
        {
            if (grounded && jump == 0)
            {
                numberOfJumps--;
            }
            grounded = false;
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
                if (rb.gravityScale < jumpApexThreshold)
                {
                    rb.gravityScale = jumpApexThreshold;
                }
            }
            else if (jump == 0)
            {
                canJump = true;
                rb.gravityScale = gravity;
            } else
            {
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
                //GetComponent<SpriteRenderer>().sprite = spr1;
            }
            else if (playerInput.playerIndex == 2)
            {
                //GetComponent<SpriteRenderer>().sprite = spr2;
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
        useAction = playerInput.actions["PowerUp"];
    }
    #endregion
    void ChangeOrientation()
    {
        if (move.x > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (move.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
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

    private void AfterHitInvincibility()
    {
        if (isInvincilbe)
        {
            invincibilityTimer -= Time.deltaTime;
        }

        if (invincibilityTimer <= 0)
        {
            isInvincilbe = false;
            invincibilityTimer = afterHitInvincibilityTime;
        }
    }

    private void decayHoney()
    {
        if (isHoneyed)
        {
            honeyedTimer -= Time.deltaTime;
        }
        if (honeyedTimer <= 0)
        {
            isHoneyed = false;
        }
    }

    private void changeSprites()
    {
        if (isHoneyed)
        {
            sprRender.color = Color.yellow;
        }
        else if (isShielded)
        {
            sprRender.color = Color.red;
        }
        else if (isInvincilbe)
        {
            sprRender.color = Color.black;
        }
        else
        {
            sprRender.color = Color.white;
        }
    }
}