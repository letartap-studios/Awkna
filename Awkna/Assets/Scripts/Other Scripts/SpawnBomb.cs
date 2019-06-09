using UnityEngine;

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

    public float updateOffset;
    public LayerMask updateMask;
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
                    PlayerStats.Instance.TakeDamage(PlayerStats.Instance.MaxHealth, transform.position);  // ...damage the player.
                }
                else if (objectsToDamage[i].CompareTag("Enemy"))     // If the bomb collides with an enemy at explosion,...
                {
                    //                                              // ...deal damage to the enemy equal to its health. (Kill it)
                    objectsToDamage[i].GetComponent<EnemyHealth>().TakeDamage(objectsToDamage[i].GetComponent<EnemyHealth>().health);
                }
                else if (objectsToDamage[i].CompareTag("Crate"))                                               // If it collides with anything else that is destructible,..
                {
                    objectsToDamage[i].GetComponent<CrateHealth>().DestroyCrate();
                }
                else if (objectsToDamage[i].CompareTag("GemTile"))
                {
                    objectsToDamage[i].GetComponent<GemTileHealth>().DestroyGemTile();
                }
                else
                {
                    Destroy(objectsToDamage[i].gameObject);
                }

                // ...destroy it.

            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeController>().Shake();

            GameObject instance = Instantiate(effect, transform.position, Quaternion.identity);   //Explosion effect.

            FindObjectOfType<AudioManager>().Play("Explosion1");

            Destroy(whatIsBomb);                                            // Destroy the bomb at explosion.
            Destroy(instance, 3f);

            Collider2D[] tilesToUpdate = Physics2D.OverlapCircleAll(transform.position, areaOfEffect + updateOffset, updateMask);

            for (int i = 0; i < tilesToUpdate.Length; i++)
            {
                if (tilesToUpdate[i].GetComponent<SpriteSelector>() != null)
                    tilesToUpdate[i].GetComponent<SpriteSelector>().ChangeSprite();
            }

            AstarPath.active.Scan();
        }
        else
        {
            timer -= Time.deltaTime; // The timer goes down each frame.
            gameObject.GetComponent<Animation>().Play("bomb ticking");
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected() // Draw a gismos circle around the area of effect.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect + updateOffset);
    }
#endif
}
