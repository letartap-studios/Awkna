using UnityEngine;

public class ExplosiveBox : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFalling;
    private bool wasFalling;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        wasFalling = isFalling; 
        if (rb.velocity.y > 0.5)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        if (isFalling == false && wasFalling == true)
        {
            Destroy(gameObject);
        }
    }
}
