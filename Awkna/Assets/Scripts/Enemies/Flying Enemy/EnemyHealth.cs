using UnityEngine;

// This script controlls the health of the enemy.
public class EnemyHealth : MonoBehaviour
{
    public int health;
    public Animator anim;
    [HideInInspector]
    public float countdownTimeToInvulnerability = 0;
    public float invulnerabilityTime = 0.5f;
    public PatrollingEnemyMovement movement;
    private bool dead = false;


    private void Update()
    {
        if (health <= 0 && dead == false)
        {
            dead = true;
            if (movement != null)
                movement.movementSpeed = 0;
            if (anim != null)
            {
                anim.SetTrigger("die");
            }
            Destroy(gameObject.transform.parent.gameObject, 0.5f);
        }
    }

    public void TakeDamage(int damage)     // When the function is called deal damage to the enemy equal to the damage parameter.
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeController>().Shake();
        health -= damage;                  // Lower the enemy's health by the damage amount.
        Debug.Log("Enemy has taken damage");
        if (anim != null)
        {
            anim.SetTrigger("takeDamage");
        }
    }
}
