using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject soldObject;

    public Sprite soldObjectSprite;

    public GameObject[] PickupArray;

    public TextMesh PriceText;

    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 size;
    private LayerMask playerMask;
    public Animator anim;
    public Animator anim2;
    public int price;
    public int numberOfItems;

    public GameObject displayItemPos;

    

    private void Start()
    {
        soldObjectSprite =  GetComponent<SpriteRenderer>().sprite;

        playerMask = LayerMask.GetMask("Player");
        soldObject = PickupArray[Random.Range(1, PickupArray.Length)];
        displayItemPos.GetComponent<SpriteRenderer>().sprite = soldObject.GetComponent<SpriteRenderer>().sprite;
        PriceText.text = price.ToString();
    }

    public void DestroyShopItem()
    {
        Instantiate(soldObject, transform.position, Quaternion.identity);
        //GameObject.FindWithTag("Player").GetComponent<RopeSystem>().ResetRope();
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
            if (anim2 != null)
            {
                anim2.SetBool("inRange", true);
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
            if (anim2 != null)
                anim2.SetBool("inRange", false);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }
#endif
}
