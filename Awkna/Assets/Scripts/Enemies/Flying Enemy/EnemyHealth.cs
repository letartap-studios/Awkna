using UnityEngine;

// This script controlls the health of the enemy.
public class EnemyHealth : MonoBehaviour
{
    public int health;
    [HideInInspector]


    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy has taken damage");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            TakeDamage(1);
            FindObjectOfType<AudioManager>().Play("hit_alien");
        }
    }
}
