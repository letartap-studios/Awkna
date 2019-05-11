using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public float healPlayer = 1;
    public Vector2 areaOfEffect;

    private void Update()
    {
        Collider2D other = Physics2D.OverlapBox(transform.position, areaOfEffect, 0f);
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerStats.Instance.Heal(healPlayer);
        }

    }

    private void OnDrawGizmosSelected() // Draw a gismos circle around the area of effect.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, areaOfEffect);
    }
}
