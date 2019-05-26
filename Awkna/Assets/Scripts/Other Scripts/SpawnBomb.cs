using UnityEngine;
using EZCameraShake;

// This script spawns the bomb when calling it from the PlayerController.cs and then explodes after a timer.

[RequireComponent(typeof(AudioSource))]
public class SpawnBomb : MonoBehaviour
{
    #region Variables
    public float timer;                     // The timer at which the bomb explodes.
    public float areaOfEffect;              // The area of effect of the explosion.
    public GameObject whatIsBomb;
    public float damagePlayer = 1f;         // The damage it deals to the player at explosion.

    public LayerMask whatIsDestructible;    // Whether the objects are destructible.
    public GameObject effect;               // Explosion effect.
    public GameObject[] obj;
    public GameObject crate;
    #endregion


    private void Update()
    {
        if (timer <= 0)   // After spawning in the player controller, ...
        {                 // ...the timer of the bomb goes down and when it reaches 0 it explodes.
                          // The bomb destroys everything in the area of effect if it destructible.
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, whatIsDestructible);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                if (objectsToDamage[i].CompareTag("Player"))        // If the bomb collides with the player at explosion, ...
                {                                                   
                    PlayerStats.Instance.TakeDamage(PlayerStats.Instance.MaxHealth);  // ...damage the player.
                }
                else if(objectsToDamage[i].CompareTag("Enemy"))     // If the bomb collides with an enemy at explosion,...
                {
                    //                                              // ...deal damage to the enemy equal to its health. (Kill it)
                    objectsToDamage[i].GetComponent<FlyingEnemyHealth>().TakeDamage(objectsToDamage[i].GetComponent<FlyingEnemyHealth>().health);
                }
                else                                                // If it collides with anything else that is destructible,...
                {
                    if (objectsToDamage[i].CompareTag("Crate"))
                    {
                        objectsToDamage[i].GetComponent<CrateHealth>().TakeDamage(1);
                    }
                    else if (objectsToDamage[i].CompareTag("GemTile"))
                    {
                        objectsToDamage[i].GetComponent<GemTileHealth>().TakeDamage(1);
                    }else
                    {
                        Destroy(objectsToDamage[i].gameObject);
                    }
                    
                    // ...destroy it.

                }
            }

            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);               // Shake the camera effect on explosion.

            GameObject instance = Instantiate(effect, transform.position, Quaternion.identity);   //Explosion effect.



            FindObjectOfType<AudioManager>().Play("Explosion1");

            Destroy(whatIsBomb);                                            // Destroy the bomb at explosion.
            Destroy(instance, 3f);
            AstarPath.active.Scan();
        }
        else
        {
            timer -= Time.deltaTime; // The timer goes down each frame.
            gameObject.GetComponent<Animation>().Play("bomb ticking");
        }
    }

    private void OnDrawGizmosSelected() // Draw a gismos circle around the area of effect.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
}
