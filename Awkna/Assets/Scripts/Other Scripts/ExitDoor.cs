using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitDoor : MonoBehaviour
{
    public Vector3 offset;
    public float range;
    public Animator anim;

    //public float waitTime = 0.1f;
    private void Update()
    {
        bool other = Physics2D.OverlapCircle(transform.position + offset, range, LayerMask.GetMask("Player"));
        if (other)
        {
            if (Input.GetButton("Interact"))
            {
                //if (waitTime <= 0)                              // If the player presses the down button for a 'waitTime' period of time, ...
                //{
                //    //     ***End Screen here***                         // ...the end level screen will appear.
                    Debug.Log("Exit");
                    SceneManager.LoadScene("Endgame");
                //    waitTime = 0f;
                //}
                //else
                //{
                //    waitTime -= Time.deltaTime;                 //decrease the period of time
                //}
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim != null)
                anim.SetBool("inRange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim != null)
                anim.SetBool("inRange", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, range);
    }
}
