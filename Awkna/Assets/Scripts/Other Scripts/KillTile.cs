using UnityEngine;

// This script kills the player when it collides with the KillTile.

public class KillTile : MonoBehaviour
{
    public float damage;
    //private Collider2D collider2D;

    public Vector3 offset;
    public Vector3 size;

    public float knockDuration = 0.5f;
    public float knockbackPwr = 2f;

    //private void Start()
    //{
    //    collider2D = GetComponent<Collider2D>();
    //}
    private PlayerController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
    
    private void Update()
    {
        bool other = Physics2D.OverlapBox(transform.position + offset, size, 0, LayerMask.GetMask("Player"));

        if (!other)
        {
            return;
        }
        else
        {
            PlayerStats.Instance.TakeDamage(damage);
            StartCoroutine(player.Knockback(knockDuration, knockbackPwr, player.transform.position, transform.position.x));

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }
}
