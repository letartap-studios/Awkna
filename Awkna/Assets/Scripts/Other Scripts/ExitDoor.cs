using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public float waitTime = 1.5f;
    private void OnTriggerEnter2D(Collider2D other) //change the on trigger enter
    {
        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0) 
        {
           // if (waitTime <= 0)                              // If the player presses the down button for a 'waitTime' period of time, ...
            //{
                //         ***End Screen here***                         // ...the end level screen will appear.
                Debug.Log("Exit");
                //waitTime = 0f;
           // }
            //else
          //  {
            //    waitTime -= Time.deltaTime;                 //decrease the period of time
          // }
        }
    }
}
