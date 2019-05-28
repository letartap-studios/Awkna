using UnityEngine;

// This script kills the player when it collides with the KillTile.

public class KillTile : MonoBehaviour
{
    public float damage;
    private Collider2D collider2D;
    private PlayerController player;

    public Vector2 offset;
    public Vector2 size;

    public float knockDuration = 0.5f;
    public float knockbackPwr = 2f;

    private LayerMask playerMask;

    //private void Start()
    //{
    //    collider2D = GetComponent<Collider2D>();
    //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //    playerMask = LayerMask.GetMask("Player");
    //}

    //private void Update()
    //{
    //    bool other = collider2D.IsTouchingLayers(playerMask);
    //    if (!other)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        //player.Knockback(knockDuration, knockbackPwr, player.transform.position, transform.position.x);
    //        PlayerStats.Instance.TakeDamage(damage);
    //    }
    //}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        bool other = Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0, playerMask);

        if (!other)
        {
            return;
        }
        else
        {
            //player.Knockback(knockDuration, knockbackPwr, player.transform.position, transform.position.x);
            PlayerStats.Instance.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }



}
