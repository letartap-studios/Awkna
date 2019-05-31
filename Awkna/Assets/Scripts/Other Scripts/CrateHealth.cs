using UnityEngine;

public class CrateHealth : MonoBehaviour
{
    public int health;
    public GameObject[] objects;
    public GameObject crate;
    public Animator anim;

    public void DestroyCrate()
    {
        Destroy(crate);
        for (int i = 1; i <= 3; i++)
        {
            int rand = Random.Range(0, objects.Length);
            Instantiate(objects[rand], transform.position, Quaternion.identity);
            AstarPath.active.Scan();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim != null)
                anim.SetBool("inRange", true);
            if (Input.GetButtonDown("Interact"))
            {
                DestroyCrate();
            }
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

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
