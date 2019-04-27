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
    public float sizeX;
    public float sizeY;
    public float posX;
    public float posY;
    public float angle;
    private Vector2 boxSize;
    private Vector2 boxPos;


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
        boxSize = new Vector2(sizeX, sizeY);
        boxPos = new Vector2(posX, posY);

        playerInRange = Physics2D.OverlapBox(boxPos, boxSize, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            aIPath.enabled = true;
           
        }
    }
    private void OnDrawGizmosSelected() // Draw a gismos circle around the ground check radius.
    {
        Gizmos.color = Color.red;
        Vector2 vec = new Vector2(posX, posY);

        Gizmos.DrawWireCube(boxPos, boxSize);
    }
}
