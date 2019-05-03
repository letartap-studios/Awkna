using UnityEngine;

public class Platform : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float waitTime;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0)
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

        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            effector.rotationalOffset = 90f;
        }


        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            effector.rotationalOffset = 270f;
        }

        if (Input.GetButtonDown("SwitchGravity"))
        {
            effector.rotationalOffset = 0;
        }
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

