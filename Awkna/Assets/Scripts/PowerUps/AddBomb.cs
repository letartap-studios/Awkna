using UnityEngine;


public class AddBomb : MonoBehaviour
{
    public float areaOfEffect;
    private void Update()
    {
        Collider2D other = Physics2D.OverlapCircle(transform.position, areaOfEffect);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().AddBomb();
            Destroy(gameObject);
        }

    }
    private void OnDrawGizmosSelected() // Draw a gismos circle around the area of effect.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
}
