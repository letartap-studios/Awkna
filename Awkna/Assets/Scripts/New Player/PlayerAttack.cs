using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;            // Wait a few miliseconds between each attack.
    public float startTimeBtwAttack;

    public Transform attackPos;             // Attack position.
    public float attackRange;               // Attack range.

    public LayerMask whatIsEnemy;           // What layers are enemies.

    public int damage;                      // The damage that the player deals to the enemies.

    private void Update()
    {
        if (timeBtwAttack <= 0)    // Then the player can attack.
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }

                timeBtwAttack = startTimeBtwAttack;
            }            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
