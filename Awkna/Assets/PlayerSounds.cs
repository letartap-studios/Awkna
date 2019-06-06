using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    bool isMoving;
    AudioSource audioSrc;
    private float timeToNextStep;
    public float minTimeBetweenSteps;

    private bool spawnDust;

    public GameObject dustEffect;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {



        if (PlayerController.Instance.isGrounded == true && PlayerController.Instance.GetHorizontalMoveInput() != 0 /*&& audioSrc.isPlaying == false*/)
        {


            audioSrc.volume = Random.Range(0.1f, 0.3f);
            audioSrc.pitch = Random.Range(0.8f,1.2f);
            if (timeToNextStep <= 0)
            {
                audioSrc.Play();
                Instantiate(dustEffect, PlayerController.Instance.groundCheck.position, Quaternion.identity);
                timeToNextStep = minTimeBetweenSteps;
            }
            else
            {
                
                timeToNextStep -= Time.deltaTime;
            }
        }





    }
}
