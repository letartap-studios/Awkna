using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public float waitTime = 1.5f;
    private void Update()
    {
        Collider2D other = Physics2D.OverlapBox(transform.position, transform.localScale, 0);
        if (other.CompareTag("Player"))
        {
            if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
            {
                if (waitTime <= 0)                              // If the player presses the down button for a 'waitTime' period of time, ...
                {
               //     ***End Screen here***                         // ...the end level screen will appear.
                    Debug.Log("Exit");
                    waitTime = 0f;
                }
                else
                {
                    waitTime -= Time.deltaTime;                 //decrease the period of time
                }
            }
        }
    }
}
