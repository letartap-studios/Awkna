using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    private float waitTime = 5f;
    private float waitTimeJump = 0.08f;
    private float waitTimePutBomb = 5f;
    private float waitTimeGem = 5f;

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                if (waitTimeJump <= 0)
                {
                    popUpIndex++;
                }
                else
                {
                    waitTimeJump -= Time.deltaTime;
                    Debug.Log(waitTimeJump);
                }
            }
        }else if (popUpIndex == 1)
        {
            if (Input.GetButtonDown("Jump"))
            {
                    popUpIndex++;
            }
        }else if (popUpIndex == 2)
        {
            if (waitTime <= 0)
            {
                popUpIndex++;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }else if (popUpIndex == 3)
        {
            if (waitTimePutBomb <= 0)
            {
                popUpIndex++;
            }
            else
            {
                waitTimePutBomb -= Time.deltaTime;
            }
        }else if (popUpIndex == 4)
        {
            if (Input.GetButtonDown("Bomb"))
            {
                if (waitTimeGem <= 0)
                {
                    popUpIndex++;
                }
                else
                {
                    waitTimeGem -= Time.deltaTime;
                }
            }
        }
    }
}
