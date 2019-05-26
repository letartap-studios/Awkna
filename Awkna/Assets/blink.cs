using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    public float time = 1;

    float countTime = 0;
    bool active = true;

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime > time)
        {
            active = !active;
            gameObject.SetActive(active);

            countTime = 0;
        }

    }
}
