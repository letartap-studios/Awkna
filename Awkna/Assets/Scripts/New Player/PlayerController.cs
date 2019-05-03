using System.Collections;
using UnityEngine;

// Script to controll the main character

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float movementSpeed = 40f;           // The speed at which the player is moving.
    public float jumpForce = 400f;              // Amount of force added when the player jumps.    
    [Range(0, .3f)]
    [SerializeField]
    private float horizontalMovementSmoothing;  // How much to smooth out the horizontal movement.
    [Range(0, .3f)]
    [SerializeField]
    private float climbingSmoothing;            // How much to smooth out the climbing.
    [HideInInspector]
    public bool facingRight = true;             // For determining which way the player is currently facing.

    private bool isGrounded;                    // Whether or not the player is grounded.
    public Transform groundCheck;               // A position marking where to check if the player is grounded.
    public float groundCheckRadius;             // Radius of the overlap circle to determine if grounded.
    public float groundCheckRadiusHorizontal;   // Horizontal offset of the ground check radius.
    public float groundCheckRadiusVertical;     // Vertical offset of the ground check radius.
    public LayerMask whatIsGround;              // A mask determining what is ground to the character.
    private bool isJumping;                     // Wheather the player is jumping or not.
    private float jumpTimeCounter;              // The remaining amount of time the player can jump.
    public float jumpTime;                      // The maximum amount of time the player can jump.

    public LayerMask whatIsLadder;              // A mask determining what is ladder to the character.
    public float ladderDistance;                // Distance between the player and the ladder at which the player can climb.
    private bool isClimbing;                    // Whether the player is climbing.
    private RaycastHit2D ladderHitInfo;         // Whether above the player is ladder.    
    public float climbSpeed = 30f;              // The speed of the character when climbing a ladder.

    private float horizontalMoveInput = 0f;     // Input for horizontal movement.
    private float verticalMoveInput;            // Input for vertical movement.

    private Rigidbody2D rb;                     // Rigidbody of the character.
    private Vector3 velocity = Vector3.zero;

    public GameObject bomb;                     // Instance of a bomb.
    public int bombsNumber;                     // The number of bombs.

    private bool top;                           // Whether the player is upside-down.

    [HideInInspector]
    public float energy;                        // Measures the amount of energy the player has.
    public float maxEnergy;                     // The maximum energy the player can have.

    enum GravityDirection { Down, Up };
    GravityDirection m_GravityDirection;        // Whether the directon is down or up
    private float initialGravity;               // The initial gravity of the character.

    public float invulnerabilityTime = 1;       // The time in seconds that the player is invulnerable after taking damage.


    #endregion

    private void Start()
    {
        energy = maxEnergy;                             // Start with max energy.
        rb = GetComponent<Rigidbody2D>();               // Get the rigidbody component from the player object.
        initialGravity = rb.gravityScale;               // Get the initial value of the gravity.
        m_GravityDirection = GravityDirection.Down;     // Initialize the gravity direction with down.
    }

    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        #region Horizontal movment

        horizontalMoveInput = Input.GetAxisRaw("Horizontal");                                         // Get the horizontal axis input.
        Vector3 targetVelocity = new Vector2(horizontalMoveInput * movementSpeed, rb.velocity.y);     // Move the character by finding the target velocity...       
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, horizontalMovementSmoothing);
        //                                                                                            // ...and then smoothing it out and applying it to the character.
        if (horizontalMoveInput > 0 && !facingRight)        // If the input is moving the player right and the player is facing left...
        {
            Flip();                                     // ... flip the player.
        }
        else if (horizontalMoveInput < 0 && facingRight)    // Otherwise if the input is moving the player left and the player is facing right...
        {
            Flip();                                     // ... flip the player.
        }

        #endregion

        #region Climbing the ladder

        // Send a raycast upwards to check if the player is on a ladder.
        ladderHitInfo = Physics2D.Raycast(transform.position, Vector2.up, ladderDistance, whatIsLadder);

        if (ladderHitInfo.collider != null)                     // Check whether the ray has collided with a ladder.
        {
            isClimbing = true;                                  // The player can climb...
        }
        else                                                    // ...else he can't.
        {
            isClimbing = false;
        }
        if (isClimbing == true)                                      // If the player is climbing...
        {
            verticalMoveInput = Input.GetAxisRaw("Vertical");        // ...get the vertical axis input and...
                                                                     // ...move the character by finding the target velocity...
                                                                     //Vector3 verticalTargetVelocity = new Vector2(rb.velocity.x, verticalMoveInput * climbSpeed);
                                                                     //                                                       // ...and then smoothing it out and applying it to the character.
                                                                     //rb.velocity = Vector3.SmoothDamp(rb.velocity, verticalTargetVelocity, ref velocity, horizontalMovementSmoothing);
            rb.velocity = new Vector2(rb.velocity.x, verticalMoveInput * climbSpeed);
            //rb.gravityScale = 0;                                     // Set the characters gravity to 0, in order to make the player climb.
        }
        else
        {
            // rb.gravityScale = initialGravity;                        // Else set the gravity back to normal.
        }

        #endregion

        #region Switch Gravity
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
        #endregion

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
    }

    private void Update()
    {
        #region Jump
        if (m_GravityDirection == GravityDirection.Down) // Check if the gravity is downwards so the jump force is up.
        {
            if (Input.GetButtonDown("Jump") && isGrounded)          // Check if the Jump button was pressed and give the player's...
            {
                rb.velocity = Vector2.up * jumpForce;               //...rigidbody velocity on the y axis.
                isJumping = true;                                   // The player is jumping.
                jumpTimeCounter = jumpTime;                         // Reset the jump time counter.
            }

            if (Input.GetButton("Jump") && isJumping == true)        // While the player is holding down the jump button...
            {
                if (jumpTimeCounter > 0)                             //...and he has jump time remaining...
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
        Physics2D.IgnoreLayerCollision(13, 15);                 // Ignore the collision between the player and the bomb.

        if (Input.GetButtonDown("Bomb") && bombsNumber > 0 && isGrounded) // If the player has more then 0 bombs remaining and he presses down
        {                                                                 // the bomb button and is grounded, then...
            Instantiate(bomb, transform.position, Quaternion.identity);   //... spawn a bomb at the player position.
            bombsNumber--;                                      // Lose one bomb from inventory.
        }
        #endregion
    }

    #region Functions
    private void Flip()     // Flip player facing when walking (left and right).
    {
        facingRight = !facingRight;                 // Switch the way the player is labelled as facing.

        Vector3 theScale = transform.localScale;    // Multiply the player's x local scale by -1.
        theScale.x *= -1;
        transform.localScale = theScale;
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

    public void AddBomb()   // When the player finds a bomb add it to the inventory.
    {
        bombsNumber++;
    }

    private void OnDrawGizmosSelected() // Draw a gismos circle around the ground check radius.
    {
        Gizmos.color = Color.red;
        Vector2 vec = new Vector2(transform.position.x + groundCheckRadiusHorizontal, transform.position.y + groundCheckRadiusVertical);

        Gizmos.DrawWireSphere(vec, groundCheckRadius);
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir, float posX)
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

        yield return 0;
    }

    public IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(12, 15, true);
        yield return new WaitForSeconds(invulnerabilityTime);
        Physics2D.IgnoreLayerCollision(12, 15, false);
    }
    #endregion
}
