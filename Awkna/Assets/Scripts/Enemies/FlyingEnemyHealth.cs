using UnityEngine;
using Pathfinding;

// This script controlls the health of the enemy.

public class FlyingEnemyHealth : MonoBehaviour
{
    public int health;            // The health of the enemy.
    private float dazedTime;
    public float startDazedTime = 0.3f;
    public float deathTime;
    //private Animator anim;
    public float deathAnimationTime;

    private void Start()
    {
        //anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (dazedTime <= 0)
        {
            GetComponent<AIPath>().enabled = true;
        }
        else
        {
            GetComponent<AIPath>().enabled = false;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)          // If the enemy reached 0 health...
        {
            GetComponent<AIPath>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            //anim.SetTrigger("Dead");
            
            if(deathAnimationTime <= 0)
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            else
            {
                deathAnimationTime -= Time.deltaTime;
            }

            if (deathTime <= 0)
            {
                Destroy(gameObject);  //... destroy it.
            }
            else
            {                
                deathTime -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)     // When the function is called deal damage to the enemy equal to the damage parameter.
    {
        dazedTime = startDazedTime;
        health -= damage;                  // Lower the enemy's health by the damage amount.
        Debug.Log("Enemy has taken damage");
    }
}
