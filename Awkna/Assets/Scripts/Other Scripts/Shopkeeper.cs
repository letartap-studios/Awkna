using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public bool lightsOut = false;

    private TextMesh message;
    private bool firstTime = true;
    private bool secondTime = true;

    private void Start()
    {
        message = GetComponent<TextMesh>();
        message.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!lightsOut)
            {
                if (firstTime)
                {
                    message.text = "Hello, earthling!";
                    firstTime = false;
                }
                else if(secondTime)
                {
                    message.text = "Hello again, earthling!";
                    secondTime = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!lightsOut)
            {
                message.text = "Farewell, creature!";
            }
        }
    }
}
