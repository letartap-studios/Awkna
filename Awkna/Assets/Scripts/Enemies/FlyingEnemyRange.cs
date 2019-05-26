using Pathfinding;
using UnityEngine;

// This script activates the AIPath script on the enemies.

public class FlyingEnemyRange : MonoBehaviour
{
    #region Variables
    private AIPath aIPath;                              // Refrence to the AIPath script on the enemy.
    private AIDestinationSetter AIDestinationSetter;    // Refrence to the AIDestinationSetter script on the enemy.
    private GameObject player;                          // Refence to the player object in the scene.
    private bool playerInRange = false;                 // Whether the player has been in range of the enemy until the moment.
    private float distanceToPlayer;                     // The distance at which the enemy starts to follow the player.
    [HideInInspector]
    public bool isFollowing;
    #endregion

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        // Find the object in the scene with the tag "Player".
        AIDestinationSetter = GetComponent<AIDestinationSetter>();  // Get the AIDestinationSetter script from the object.
        AIDestinationSetter.target = player.transform;              // Set the target of the enemy to the object Player.
        distanceToPlayer = 6f;                                      // Hardcoded the value because it resets when the enemy enstantiates.        
    }

    private void Update()
    {
        // Check if the player is under the enemy and if the player was in range of the enemy until this moment...
        if ((player.transform.position.y < transform.position.y) && playerInRange == false)
        {
            //... then use the Pitagorean theorem in a cartesian coordinate system to calculate...
            //... the distance between the player and the enemy.
            // distance = Sqrt( ( xA - xB )^2 + (yA - yB)^2 );
            float distance = Mathf.Sqrt((player.transform.position.x - transform.position.x) * (player.transform.position.x - transform.position.x) +
                (player.transform.position.y - transform.position.y) * (player.transform.position.y - transform.position.y));

            if (distance < distanceToPlayer)
            {
                // The player is in range now.
                playerInRange = true;
            }
        }

        if (playerInRange)          // If the player was in range of the enemy...
        {
            GetComponent<AIPath>().enabled = true;  // Activate the script AIPath.
            GetComponent<FlyingEnemyHealth>().enabled = true;
            GetComponent<EnemyAttack>().enabled = true;
            GetComponent<FlyingEnemyRange>().enabled = false;
        }

    }
}
