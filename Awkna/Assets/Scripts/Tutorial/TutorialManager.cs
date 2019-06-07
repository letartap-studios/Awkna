using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public GameObject[] collider;

    void Update()
    {
        Debug.Log(popUpIndex);
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

        /*if (popUpIndex == 7)
        {
            if (Input.GetButtonDown("Grapple"))
            {
                popUpIndex++;
            }
        }*/

        /*if (popUpIndex == 8)
        {
            popUpIndex++;
        }*/
        /*
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
        }else if (popUpIndex == 5)
        {
            if (collider.CompareTag("Player"))
            {
                popUpIndex++;
            }
        }*/
    }

    public void increment()
    {
        Destroy(collider[popUpIndex]);
        popUpIndex++;
    }
        
}