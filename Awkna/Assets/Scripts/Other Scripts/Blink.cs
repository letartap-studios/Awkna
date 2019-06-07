using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

    AudioSource audioSrc;
    public float time = 1;
    public bool active = true;
    public float paddingTIme = 0f;

    float countTime = 0;
    GameObject go;

    private void Start()
    {

        audioSrc = GetComponent<AudioSource>();

        time += paddingTIme;

        go = this.gameObject.transform.GetChild(0).gameObject;

    }



    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime > time)
        {
            active = !active;
            go.SetActive(active);

            countTime = 0;
        }

        if (go.activeSelf && !audioSrc.isPlaying)
        {
            audioSrc.Play();
        }
        else
        {
            audioSrc.Stop();
        }


    }
}
