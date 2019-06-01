using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Text textf;

    private GameObject obj;

    public void AppearDialogue(string String)
    {

        textf.text = String;
        //textf.enabled = true;
        textf.gameObject.SetActive(true);
        
    }

}
