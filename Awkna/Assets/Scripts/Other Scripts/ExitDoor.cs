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
                Debug.Log("Next Level");
                SceneManager.LoadScene("Endgame");
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
