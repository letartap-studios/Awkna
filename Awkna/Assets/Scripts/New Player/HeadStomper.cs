using UnityEngine;

public class HeadStomper : MonoBehaviour
{
    public float bounceOnEnemy;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        anim = transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(collision.GetComponent<EnemyHealth>().health);
            anim.SetTrigger("takeOf");
            rb.velocity = new Vector2(rb.velocity.x, bounceOnEnemy);
            
        }
    }
}
