using UnityEngine;

// This script kills the player when it collides with the KillTile.

public class KillTile : MonoBehaviour
{
    public float damage;
    private Collider2D collider2D;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }
    
    private void Update()
    {
        bool other = collider2D.IsTouchingLayers(LayerMask.GetMask("Player"));
        if (!other)
        {
            return;
        }
        else
        {
            PlayerStats.Instance.TakeDamage(damage);
        }
    }
}
