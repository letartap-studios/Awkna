using UnityEngine;

// This script kills the player when it collides with the KillTile.

public class KillTile : MonoBehaviour
{
    public float damage;
    public Vector2 offset;
    public Vector2 size;

    private LayerMask playerMask;

    private void Start()
    {
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
            PlayerStats.Instance.TakeDamage(damage);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }
#endif


}
