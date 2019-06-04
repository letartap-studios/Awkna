using UnityEngine;
using EZCameraShake;

public class ExplosiveBox : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool wasGrounded;

    public float areaOfEffect;
    public LayerMask whatIsDestructible;
    public float damagePlayer;

    public Vector2 offset;
    public Vector2 size;
    public GameObject effect;

    public LayerMask whatIsGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapBox(offset, size, whatIsGround);

        if(!wasGrounded && isGrounded)
        {
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, whatIsDestructible);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                if (objectsToDamage[i].CompareTag("Player"))        // If the bomb collides with the player at explosion, ...
                {
                    PlayerStats.Instance.TakeDamage(damagePlayer, transform.position);  // ...damage the player.
                }
                else if (objectsToDamage[i].CompareTag("Enemy"))     // If the bomb collides with an enemy at explosion,...
                {
                    //                                              // ...deal damage to the enemy equal to its health. (Kill it)
                    objectsToDamage[i].GetComponent<EnemyHealth>().TakeDamage(objectsToDamage[i].GetComponent<EnemyHealth>().health);
                }
                else                                                // If it collides with anything else that is destructible,...
                {
                    Destroy(objectsToDamage[i].gameObject);         // ...destroy it.
                }
            }

            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);               // Shake the camera effect on explosion.

            Instantiate(effect, transform.position, Quaternion.identity);   //Explosion effect.

            Destroy(gameObject);                                            // Destroy the bomb at explosion.

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(offset, size);
    }
}
