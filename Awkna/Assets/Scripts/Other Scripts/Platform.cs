using UnityEngine;

public class Platform : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float radius;
    public Vector2 offset;
    public LayerMask player;
    private Collider2D collider2D;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CheckCollision();
    }

    private void CheckCollision()
    {
        bool other = collider2D.IsTouchingLayers(player);
        if (!other)
        {
            return;
        }
        else
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                effector.rotationalOffset = 180f;
            }                                                 //if the player hits the Down button, the collision platform rotates 180 degrees,
            else                                              //letting the player to fall. Otherwise, the platform collision will only permit 
            {                                                 //jumping on it
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
}
