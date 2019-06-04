using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTalkScript : MonoBehaviour
{
    AudioSource audioSrc;
    private float timeToNextStep;
    public float minTimeBetweenSteps;

    // Start is called before the first frame update
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = Random.Range(0.1f, 0.3f);
        audioSrc.pitch = Random.Range(0.9f, 1.6f);
        minTimeBetweenSteps = Random.Range(4f, 6f);
        if (timeToNextStep <= 0)
        {
            if(audioSrc.isPlaying == false)
            {
                audioSrc.Play();
            }
            timeToNextStep = minTimeBetweenSteps;
        }
        else
        {
            timeToNextStep -= Time.deltaTime;
        }
    }
}
