using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject[] objects;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 size;
    private LayerMask playerMask;
    public Animator anim;
    public int price;
    public int numberOfItems;

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
    }

    public void DestroyShopItem()
    {
        for (int i = 1; i <= numberOfItems; i++) 
        {
            int rand = Random.Range(0, objects.Length);
            Instantiate(objects[rand], transform.position, Quaternion.identity);
            //AstarPath.active.Scan();
        }
        GameObject.FindWithTag("Player").GetComponent<RopeSystem>().ResetRope();
        Destroy(gameObject);
    }

    private void Update()
    {
        bool other = Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0, playerMask);

        if (!other)
        {
            return;
        }
        else
        {
            if (anim != null)
            {
                anim.SetBool("inRange", true);
            }

            if (Input.GetButtonDown("Interact"))
            {
                if (PlayerStats.Instance.GemNumber >= price)
                {
                    PlayerStats.Instance.PayGems(price);
                    DestroyShopItem();
                    
                }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }
}
