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
    [HideInInspector]
    public bool attackTrigger;

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        attackTrigger = Physics2D.OverlapCircle((Vector2)transform.position + offset, radius, playerMask);

        if (attackTrigger)
        {
            PlayerStats.Instance.TakeDamage(dealtDamage, transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
    }

}

