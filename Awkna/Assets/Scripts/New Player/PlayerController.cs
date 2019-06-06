using UnityEngine;

// Script to controll the main character

public class PlayerController : MonoBehaviour
{
    #region Sigleton
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerController>();
            return instance;
        }
    }
    #endregion

    #region Variables
    public float movementSpeed = 40f;           // The speed at which the player is moving.
    public float jumpForce = 400f;              // Amount of force added when the player jumps.    
    [Range(0, .3f)]
    [SerializeField]
    private float horizontalMovementSmoothing;  // How much to smooth out the horizontal movement.
    public bool facingRight = false;            // For determining which way the player is currently facing.
    [HideInInspector]
    public bool isGrounded;                     // Whether or not the player is grounded.
    public Transform groundCheck;               // A position marking where to check if the player is grounded.
    public float groundCheckRadius;             // Radius of the overlap circle to determine if grounded.
    public float groundCheckRadiusHorizontal;   // Horizontal offset of the ground check radius.
    public float groundCheckRadiusVertical;     // Vertical offset of the ground check radius.
    public LayerMask whatIsGround;              // A mask determining what is ground to the character.
    [HideInInspector]
    public bool isJumping;                      // Wheather the player is jumping or not.
    private float jumpTimeCounter;              // The remaining amount of time the player can jump.
    public float jumpTime;                      // The maximum amount of time the player can jump.

    public LayerMask whatIsLadder;              // A mask determining what is ladder to the character.
    public float ladderDistance;                // Distance between the player and the ladder at which the player can climb.
    private bool isClimbing;                    // Whether the player is climbing.
    public float climbSpeed = 30f;              // The speed of the character when climbing a ladder.
    private bool ladderCollider;
    public Vector2 ladderOffset;

    private float horizontalMoveInput = 0f;    // Input for horizontal movement.
    private float verticalMoveInput;            // Input for vertical movement.

    private Rigidbody2D rb;                     // Rigidbody of the character.
    private Vector3 velocity = Vector3.zero;


    public GameObject bomb;                     // Instance of a bomb.

    private bool top;                           // Whether the player is upside-down.

    [HideInInspector]
    public float energy;                        // Measures the amount of energy the player has.
    public float maxEnergy;                     // The maximum energy the player can have.

    enum GravityDirection { Down, Up };
    GravityDirection m_GravityDirection;        // Whether the directon is down or up
    private float initialGravity;               // The initial gravity of the character.

    public float invulnerabilityTime = 1;       // The time in seconds that the player is invulnerable after taking damage.

    [HideInInspector]
    public bool isSwinging;                     // Wheather the player is using the grappling hook at the moment.
    public Vector2 ropeHook;                    // Whichever position the rope grappling anchor is currently at.
    public float swingForce = 4f;               // A value to be used to add to the swing motion.

    public bool switchGravityPower;             // Turn on or off the gravity switch ability.
    public GameObject energyUI;                 // Turn on or off the energy bar from the UI.  

    public Animator Animator { get; private set; }
    public float GetHorizontalMoveInput()
    {
        return horizontalMoveInput;
    }
    #endregion


    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("levelstart");
        energy = maxEnergy;                             // Start with max energy.
        rb = GetComponent<Rigidbody2D>();               // Get the rigidbody component from the player object.
        Animator = GetComponent<Animator>();            // Get the animator component from the player object.
        initialGravity = rb.gravityScale;               // Get the initial value of the gravity.
        m_GravityDirection = GravityDirection.Down;     // Initialize the gravity direction with down.
        Physics2D.IgnoreLayerCollision(15, 20, true);   // Ignore the collition between the player and the collectables.
        //Physics2D.IgnoreLayerCollision(20, 20, true); // Ignore the collision between the collectables.
        Physics2D.IgnoreLayerCollision(15, 12, true);   // player, enemies
        Physics2D.IgnoreLayerCollision(13, 15, true);   // Ignore the collision between the player and the bomb.
    }

    private void Update()
    {
        // Get the horizontal axis input.
        horizontalMoveInput = Input.GetAxisRaw("Horizontal");

        #region Flip the player facing by mouse cursor
        // using mousePosition and player's transform (on orthographic camera view)
        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (delta.x >= 0 && !facingRight)
        { // mouse is on right side of player
            transform.localScale = new Vector3(-1, 1, 1); // or activate look right some other way
            facingRight = true;
        }
        else if (delta.x < 0 && facingRight)
        { // mouse is on left side
            transform.localScale = new Vector3(1, 1, 1); // activate looking left
            facingRight = false;
        }
        #endregion

        #region Jump
        if (m_GravityDirection == GravityDirection.Down)
        {
            if (Input.GetButtonDown("Jump") && (isGrounded || isSwinging || isClimbing))
            {
                Animator.SetTrigger("takeOf");
                rb.velocity = Vector2.up * jumpForce;
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }

            if (Input.GetButton("Jump") && isJumping == true)       // While the player is holding down the jump button...
            {
                if (jumpTimeCounter > 0)                            //...and he has jump time remaining...
                {
                    rb.velocity = Vector2.up * jumpForce;           //...give the player's rigidbody velocity on the y axis.
                    jumpTimeCounter -= Time.deltaTime;              // The jump time goes down each frame the player is jumping.
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }

            if (isGrounded || isSwinging)
            {
                Animator.SetBool("isJumping", false);
            }
            else
            {
                Animator.SetBool("isJumping", true);
            }
        }
        else                                             // The same things apply to the reversed gravity.
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = Vector2.down * jumpForce;
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }

            if (Input.GetButton("Jump") && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.down * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
        }
        #endregion

        #region Bomb
        if (Input.GetButtonDown("Bomb") && PlayerStats.Instance.BombsNumber > 0) // If the player has more then 0 bombs remaining and he presses down
        {                                                                 // the bomb button and is grounded, then...
            Instantiate(bomb, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);   //... spawn a bomb at the player position.
            PlayerStats.Instance.LoseBomb();
        }
        #endregion
    }



    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        #region Horizontal movement

        #region Player facing
        //if (horizontalMoveInput > 0 && !facingRight)
        //{
        //    Flip();
        //}
        //else if (horizontalMoveInput < 0 && facingRight)
        //{
        //    Flip();
        //}
        #endregion

        #region Swinging
        if (isSwinging && Mathf.Abs(horizontalMoveInput) > 0f)
        {
            Physics2D.IgnoreLayerCollision(15, 19, true); // player, crate
            //animator.SetBool("IsSwinging", true);

            // Get a normalized direction vector from the player to the hook point
            Vector2 playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

            // Inverse the direction to get a perpendicular direction
            Vector2 perpendicularDirection;
            if (horizontalMoveInput < 0)
            {
                perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                Vector2 leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
            }
            else
            {
                perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                Vector2 rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
            }

            Vector2 force = perpendicularDirection * swingForce;
            rb.AddForce(force, ForceMode2D.Force);
        }

        if (!isSwinging)
        {
            //animator.SetBool("IsSwinging", false);
            Physics2D.IgnoreLayerCollision(15, 19, false);


            Vector3 targetVelocity = new Vector2(horizontalMoveInput * movementSpeed, rb.velocity.y);     // Move the character by finding the target velocity...       
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, horizontalMovementSmoothing);
        }                                                                                          // ...and then smoothing it out and applying it to the character.
        #endregion

        if (horizontalMoveInput == 0)
        {
            Animator.SetBool("isRunning", false);
        }
        else if (isGrounded)
        {
            Animator.SetBool("isRunning", true);
        }

        #endregion

        #region Climbing the ladder

        if (!isSwinging)
        {
            Physics2D.IgnoreLayerCollision(15, 11, false); // player, ladders

            ladderCollider = Physics2D.OverlapCircle((Vector2)transform.position + ladderOffset, ladderDistance, whatIsLadder);

            if (ladderCollider)
            {
                isClimbing = true;

                verticalMoveInput = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, verticalMoveInput * climbSpeed);
                rb.gravityScale = 0;
            }
            else
            {
                isClimbing = false;
                rb.gravityScale = initialGravity;
            }
        }
        else
        {
            rb.gravityScale = initialGravity;

            Physics2D.IgnoreLayerCollision(15, 11, true); // player, ladders
        }
        #endregion

        #region Switch Gravity
        if (switchGravityPower == true)
        {
            if (energyUI != null)
            {
                energyUI.SetActive(true);
            }

            switch (m_GravityDirection)
            {
                case GravityDirection.Down:
                    rb.gravityScale = initialGravity;                         // Change the gravity to be in a downward direction (default).
                    if (Input.GetButtonDown("SwitchGravity") && isGrounded)   // Press the switch gravity button to change the direction of gravity.
                    {
                        m_GravityDirection = GravityDirection.Up;
                        Rotation();              // Rotate the player so the controls remain the same.
                        energy--;                                             // Each time the player changes gravity loses energy.
                    }

                    break;

                case GravityDirection.Up:
                    if (energy > 0)                                             // Switch gravity only if the player has energy.
                    {
                        rb.gravityScale = -initialGravity;                      // Change the gravity to be in an upward direction
                        if (Input.GetButtonDown("SwitchGravity") && isGrounded) // Press the switch gravity button to change the direction of gravity
                        {
                            m_GravityDirection = GravityDirection.Down;
                            Rotation();
                        }
                    }
                    break;
            }
        }
        else
        {
            if (energyUI != null)
            {
                energyUI.SetActive(false);
            }
        }

        #region Energy

        if (m_GravityDirection == GravityDirection.Up)  // If the player's gravity is upwards...
        {
            energy -= Time.deltaTime;                   //...he loses energy.
            if (energy < 0)                             // If the player doesn't have energy anymore...
            {
                energy = 0;
                m_GravityDirection = GravityDirection.Down; //...chage the gravity back to normal.
                Rotation();
            }
        }
        else if (energy < maxEnergy) // If the energy is less then the maximum amount...
        {
            energy += Time.deltaTime;//...it increases over time.
        }

        #endregion

        #endregion
    }

    #region Functions
    private void Flip()     // Flip player facing when walking (left and right).
    {
        facingRight = !facingRight;                 // Switch the way the player is labelled as facing.

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;


        //transform.Rotate(0f, 180f, 0f);
    }

    void Rotation()
    {
        if (top == false)                                       // If the player is upside-down...
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);    // ...rotate on the z axis.
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        top = !top;
        facingRight = !facingRight;                             // Change the facing upon rotation

    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected() // Draw a gismos circle around the ground check radius.
    {
        Gizmos.color = Color.red;
        Vector2 vec = new Vector2(transform.position.x + groundCheckRadiusHorizontal, transform.position.y + groundCheckRadiusVertical);

        Gizmos.DrawWireSphere(vec, groundCheckRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + ladderOffset, ladderDistance);
    }
#endif
    public void Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir, float posX)
    {
        float timer = 0;        // The time that has passed since the function started.

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            if (posX <= transform.position.x)
            {
                rb.AddForce(new Vector3(knockbackDir.x * knockbackPwr, knockbackDir.y, transform.position.z));
            }
            else
            {
                rb.AddForce(new Vector3(knockbackDir.x * (-knockbackPwr), knockbackDir.y, transform.position.z));
            }
        }
    }

    #endregion
}
