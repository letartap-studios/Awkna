using UnityEngine;

// This script controlls the health of the enemy.

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)     // When the function is called deal damage to the enemy equal to the damage parameter.
    {
        health -= damage;                  // Lower the enemy's health by the damage amount.
        Debug.Log("Enemy has taken damage");
    }
}
