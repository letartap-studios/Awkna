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
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeController>().Shake();
        Debug.Log("Enemy has taken damage");
    }
}
