using UnityEngine;
using System.Collections;

public class StartPlayer : MonoBehaviour
{
    void Start()
    {
        GameObject.FindWithTag("Player").transform.position = transform.position;
        StartCoroutine(StartScan(1f));
    }

    public IEnumerator StartScan(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        AstarPath.active.Scan();
    }
}
