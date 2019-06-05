using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;            // Time between attacks, so the player can't spam the attacks.
    public float startTimeBtwAttack;

    public Transform attackPos;             // Attack position.
    public float attackRange;               // Attack range.

    public LayerMask whatIsEnemy;           // What layers are enemies.

    public int damage;                      // The damage that the player deals to the enemies.
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)    // Then the player can attack.
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //CameraShaker.Instance.ShakeOnce(1f, 2f, .1f, .3f);   // When the player is attacking shake the camera.

                animator.SetTrigger("attacked");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                for (int i = 0; i < enemiesToDamage.Length; i++)     // Damage all enemies in the area.
                {
                    if (enemiesToDamage[i].CompareTag("Enemy"))
                    {
                        enemiesToDamage[i].transform.GetChild(0).GetComponent<EnemyHealth>().TakeDamage(damage);
                    }
                    if (enemiesToDamage[i].CompareTag("PatrollingEnemy"))
                    {
                        enemiesToDamage[i].GetComponent<PatrolEnemyHealth>().TakeDamage(damage);
                    }
                    if (enemiesToDamage[i].CompareTag("VerticalPatrollingEnemy"))
                    {
                        enemiesToDamage[i].GetComponent<VerticalPatrollingEnemyHealth>().TakeDamage(damage);
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
