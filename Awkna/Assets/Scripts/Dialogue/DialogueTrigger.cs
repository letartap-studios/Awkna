using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;

    public void AppearDialogue(string String)
    {
        if (text1.activeSelf == true)
        {
            if (text2.activeSelf == true)
            {
                if (text3.activeSelf == true)
                {
                    if (text4.activeSelf == true)
                    {
                        if (text5.activeSelf == true)
                        {
                            return;
                        }
                        else
                        {
                            text5.GetComponent<CollectText>().collectText.text = String;
                            text5.SetActive(true);
                        }
                    }
                    else
                    {
                        text4.GetComponent<CollectText>().collectText.text = String;
                        text4.SetActive(true);
                    }
                }
                else
                {
                    text3.GetComponent<CollectText>().collectText.text = String;
                    text3.SetActive(true);
                }
            }
            else
            {
                text2.GetComponent<CollectText>().collectText.text = String;
                text2.SetActive(true);
            }
        }
        else
        {
            text1.GetComponent<CollectText>().collectText.text = String;
            text1.SetActive(true);
        }

    }

    //public void AppearDialogue(string String)
    //{
    //    text1.SetActive(true);
    //    text1.GetComponent<CollectText>().collectText.text = String;

    //}

}
