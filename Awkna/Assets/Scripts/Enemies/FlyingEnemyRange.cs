using Pathfinding;
using UnityEngine;

public class FlyingEnemyRange : MonoBehaviour
{
    private AIPath aIPath;
    private AIDestinationSetter AIDestinationSetter;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aIPath = GetComponent<AIPath>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
        AIDestinationSetter.target = player.transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            aIPath.enabled = true;
           
        }
    }
}
