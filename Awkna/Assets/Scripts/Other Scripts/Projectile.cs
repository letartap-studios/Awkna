using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 target;
    public float speed;
    //public GameObject effect;

    public float areaOfEffect;
    public LayerMask whatIsDestructible;
    //public int damage;

    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            //Instantiate(effect, transform.position, Quaternion.identity); //tile destruction animation
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Environment"))
        {
            Collider2D[] objectsToDestroy = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, whatIsDestructible);
            for (int i = 0; i < objectsToDestroy.Length; i++)
            {
                //objectsToDamage[i].GetComponent<DestructableEnvy>().health -= damage;
                Destroy(objectsToDestroy[i].gameObject);
            }



            //Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
#endif
}
