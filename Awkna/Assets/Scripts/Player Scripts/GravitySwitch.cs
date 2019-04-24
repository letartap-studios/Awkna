using UnityEngine;

// This script is for changing the players gravity in the opposite direction.
// This should be on the Player object.

public class GravitySwitch : MonoBehaviour
{
    enum GravityDirection { Down, Up };
    GravityDirection m_GravityDirection;                        // Whether the directon is donw or up
    private PlayerController playerController;

    private Rigidbody2D rb;

    private bool top;

    void Start()
    {
        m_GravityDirection = GravityDirection.Down;             //Initialize the gravity direction with down
        playerController = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        switch (m_GravityDirection)
        {
            case GravityDirection.Down:
                //Change the gravity to be in a downward direction (default)
                Physics2D.gravity = new Vector2(0, -9.8f);
                //Press the switch gravity button to change the direction of gravity
                if (Input.GetButtonDown("SwitchGravity"))
                {
                    m_GravityDirection = GravityDirection.Up;
                    Rotation();
                }
                break;

            case GravityDirection.Up:
                //Change the gravity to be in an upward direction
                Physics2D.gravity = new Vector2(0, 9.8f);
                //Press the switch gravity button to change the direction of gravity
                if (Input.GetButtonDown("SwitchGravity"))
                {
                    m_GravityDirection = GravityDirection.Down;
                    Rotation();
                }
                break;
        }

    }

    void Rotation()
    {
        if(top == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        //playerController.facingRight = !playerController.facingRight;
        top = !top;
    }
}
