using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerController player;
    public float knockDuration = 0.5f;
    public float knockbackPwr = 5;
    public float dealtDamage = 1;
    public Vector2 offset;
    public float radius;
    private LayerMask playerMask;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        bool attackTrigger = Physics2D.OverlapCircle((Vector2)transform.position + offset, radius, playerMask);

        if (attackTrigger)
        {
            PlayerStats.Instance.TakeDamage(dealtDamage);

            PlayerController.Instance.Knockback(knockDuration, knockbackPwr, player.transform.position, transform.position.x);

            StartCoroutine(player.GetInvulnerable());
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        PlayerStats.Instance.TakeDamage(dealtDamage);

    //        PlayerController.Instance.Knockback(knockDuration, knockbackPwr, player.transform.position, transform.position.x);

    //        StartCoroutine(player.GetInvulnerable());
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
    }

}
