using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    public GameObject obj;

    void OnTriggerEnter2D(Collider2D pui)
    {
        if (pui.CompareTag("Player"))
        {
            obj.GetComponent<TutorialManager>().increment();
        }
    }
}
