using UnityEngine;
using EZCameraShake;

// This script spawns the bomb when calling it from the PlayerController.cs and then explodes after a timer.

public class SpawnBomb : MonoBehaviour
{
    #region Variables
    public float timer;                     // The timer at which the bomb explodes.
    public float areaOfEffect;              // The area of effect of the explosion.
    public GameObject whatIsBomb;
    public float damagePlayer = 1f;         // The damage it deals to the player at explosion.

    public LayerMask whatIsDestructible;    // Whether the objects are destructible.
    public GameObject effect;               // Explosion effect.
    public Transform player;                // Transform of the player.

    #endregion

    private void Start()
    {
        damagePlayer /= 2; // divide the damage by 2 bc. the damage the player takes is doubled
    }

    private void Update()
    {
        if (timer <= 0)   // After spawning in the player controller, 
        {                 // the timer of the bomb goes down and when it reaches 0 it explodes.
                          // The bomb destroys everything in the area of effect if it destructible.
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, whatIsDestructible);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                if (objectsToDamage[i].CompareTag("Player"))        // If the bomb collides with the player at explosion, ...
                {                                                   
                    PlayerStats.Instance.TakeDamage(damagePlayer);  // ...damage the player.
                }
                else                                                // If it collides with anything else that is destructible,...
                {
                    Destroy(objectsToDamage[i].gameObject);         // ...destroy it.
                }
            }

            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);               // Shake the camera effect on explosion.

            Instantiate(effect, transform.position, Quaternion.identity);   //Explosion effect.

            Destroy(whatIsBomb);                                            // Destroy the bomb at explosion.

        }
        else
        {
            timer -= Time.deltaTime; // The timer goes down each frame.
        }
    }

    private void OnDrawGizmosSelected() // Draw a gismos circlie around the area of effect.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
}
