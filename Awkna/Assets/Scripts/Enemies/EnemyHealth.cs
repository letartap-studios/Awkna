using UnityEngine;
using EZCameraShake;

// This script controlls the health of the enemy.

public class EnemyHealth : MonoBehaviour
{
    public int health;            // The health of the enemy.

    private void Update()
    {
        if (health <= 0)          // If the player reaked 0 health...
        {
            Destroy(gameObject);  //... destroy it.
        }
    }

    public void TakeDamage(int damage)     // When the function is called deal damage to the enemy equal to the damage parameter.
    {
        CameraShaker.Instance.ShakeOnce(1f, 2f, .1f, .3f);   // When the enemy is taking damage shake the camera.
        health -= damage;                  // Lower the enemy's health by the damage amount.
        Debug.Log("Enemy has taken damage");
    }
}
