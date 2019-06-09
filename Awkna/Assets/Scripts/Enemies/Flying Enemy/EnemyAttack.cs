using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float dealtDamage = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(dealtDamage, transform.position);
        }
    }

}

