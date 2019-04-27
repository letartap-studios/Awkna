using Pathfinding;
using UnityEngine;

public class FlyingEnemyRange : MonoBehaviour
{
    private AIPath aIPath;                              // Refrence to the AIPath script on the enemy.
    private AIDestinationSetter AIDestinationSetter;    // Refrence to the AIDestinationSetter script on the enemy.
    private GameObject player;                          // Refence to the player object in the scene.
    private bool playerInRange;
    public LayerMask whatIsPlayer;
    public float range;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aIPath = GetComponent<AIPath>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
        AIDestinationSetter.target = player.transform;
    }

    private void FixedUpdate()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, )
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            aIPath.enabled = true;
           
        }
    }
}
