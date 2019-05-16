using UnityEngine;

public class Platform : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float radius;
    public Vector2 offset;
    public LayerMask player;
    private Vector2 pos;


    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        pos = transform.position;
    }

    private void Update()
    {
        Collider2D other = Physics2D.OverlapCircle(pos + offset, radius);
        if (other.CompareTag("Player"))
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                effector.rotationalOffset = 180f;
            }                                                                   //if the player hits the Down button, the collision platform rotates 180 degrees,
            else                                                                //letting the player to fall. Otherwise, the platform collision will only permit 
            {                                                                   //jumping on it
                effector.rotationalOffset = 0f;
            }

            if (Input.GetButton("Jump"))
            {
                effector.rotationalOffset = 0f;
            }

            if (Input.GetButtonDown("SwitchGravity"))
            {
                effector.rotationalOffset = 0;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(pos + offset, radius);
    }
}

/*public class Platform : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float waitTime;                                  //variable that sets the time the player needs to press down before he can fall trough the platform

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {                                                   //if the Down button is pressed, the 'waitTime' period of time resets
            waitTime = 0f;
        }

        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            if (waitTime <= 0)                              //if the player presses the down button for a 'waitTime' period of time, the platform collision will change
            {
                //changingTime = 0;
                effector.rotationalOffset = 180f;           //the platform collision changes with 180 degrees
                waitTime = 0f;
            }
            else
            {
                waitTime -= Time.deltaTime;                 //decrease the period of time
            }
        }

        if (Input.GetButtonDown("Jump"))                        //if the player hits Space, the platform collision returns to its starting position (0 degrees)
        {
            effector.rotationalOffset = 0;                  //the platform collision returns
        }

        if (Input.GetButtonDown("SwitchGravity"))           //
        {
            effector.rotationalOffset = 0;
        }
        

    }
}*/

