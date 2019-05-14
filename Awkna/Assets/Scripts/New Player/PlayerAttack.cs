using EZCameraShake;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;            // Time between attacks, so the player can't spam the attacks.
    public float startTimeBtwAttack;

    public Transform attackPos;             // Attack position.
    public float attackRange;               // Attack range.

    public LayerMask whatIsEnemy;           // What layers are enemies.

    public int damage;                      // The damage that the player deals to the enemies.
    public GameObject crate;
    private void Update()
    {
        if (timeBtwAttack <= 0)    // Then the player can attack.
        {
            if (Input.GetButtonDown("Fire1"))
            {
                CameraShaker.Instance.ShakeOnce(1f, 2f, .1f, .3f);   // When the player is attacking shake the camera.

                //                                                   // The range at which the player deals damage.
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                for (int i = 0; i < enemiesToDamage.Length; i++)     // Damage all enemies in the area.
                {
                    if (enemiesToDamage[i].CompareTag("Enemy"))
                    {
                        enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                    }

                    if (enemiesToDamage[i].CompareTag("Crate"))
                    {
                        enemiesToDamage[i].GetComponent<CrateHealth>().TakeDamage(damage);
                    }
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
