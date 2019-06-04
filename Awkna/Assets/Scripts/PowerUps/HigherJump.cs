using System.Collections;
using UnityEngine;

public class HigherJump : PowerUp
{
    private PlayerController player;
    public float time;
    protected override void Start()
    {
        base.Start();
        
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
    }

    protected override void Update()
    {
        base.Update();
        
        if (powerUpState == PowerUpState.IsCollected)
        {
            if (Input.GetButtonDown("Special ability"))
            {
                
                PowerUpHasExpired ();
            }
        }
    }

    public IEnumerator higherJump(float waitTime, float gravityScale, float jumpForce)
    {
        float initialGravityScale = player.GetComponent<Rigidbody2D>().gravityScale;
        float initialJumpForce = player.jumpForce;
        
        
        player.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        player.jumpForce = jumpForce;
        
        
        yield return new WaitForSeconds(waitTime);

        player.GetComponent<Rigidbody2D>().gravityScale = initialGravityScale;
        player.jumpForce = initialJumpForce;

    }
}
