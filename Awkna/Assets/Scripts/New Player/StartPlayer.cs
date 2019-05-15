using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    void Start()
    {
        GameObject.FindWithTag("Player").transform.position = transform.position;
    }
}
