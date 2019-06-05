using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject collectText1;
    public GameObject collectText2;
    public GameObject collectText3;
    public GameObject collectText4;
    public GameObject collectText5;
    public GameObject oxygenLevel;

    public void CollectDialogue(string message)
    {
        if (collectText1.activeSelf == true)
        {
            if (collectText2.activeSelf == true)
            {
                if (collectText3.activeSelf == true)
                {
                    if (collectText4.activeSelf == true)
                    {
                        if (collectText5.activeSelf == true)
                        {
                            return;
                        }
                        else
                        {
                            collectText5.GetComponent<CollectText>().collectText.text = message;
                            collectText5.SetActive(true);
                        }
                    }
                    else
                    {
                        collectText4.GetComponent<CollectText>().collectText.text = message;
                        collectText4.SetActive(true);
                    }
                }
                else
                {
                    collectText3.GetComponent<CollectText>().collectText.text = message;
                    collectText3.SetActive(true);
                }
            }
            else
            {
                collectText2.GetComponent<CollectText>().collectText.text = message;
                collectText2.SetActive(true);
            }
        }
        else
        {
            collectText1.GetComponent<CollectText>().collectText.text = message;
            collectText1.SetActive(true);
        }
    }

    public void OxygenLevelDialogueOn(string message)
    {
        oxygenLevel.GetComponent<Text>().text = message;
        if (!oxygenLevel.activeSelf)
        {
            oxygenLevel.SetActive(true);
        }
    }

    public void OxygenLevelDialogueOff()
    {
        if (oxygenLevel.activeSelf)
        {
            oxygenLevel.SetActive(false);
        }
    }

}
