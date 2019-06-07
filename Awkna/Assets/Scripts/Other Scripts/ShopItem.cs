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
    private int price;
    public int numberOfItems;

    public GameObject displayItemPos;
    public GameObject pushButtonIndicator;
    public GameObject itemCost;

    bool empty;


    private void Start()
    {        
        playerMask = LayerMask.GetMask("Player");
        EnableShop();
    }

    public void EnableShop()
    {
        soldObjectSprite = GetComponent<SpriteRenderer>().sprite;
        soldObject = PickupArray[Random.Range(1, PickupArray.Length)];
        price = soldObject.GetComponent<PowerUp>().price;
        displayItemPos.GetComponent<SpriteRenderer>().sprite = soldObject.GetComponent<SpriteRenderer>().sprite;
        displayItemPos.GetComponent<SpriteRenderer>().enabled = true;
        pushButtonIndicator.SetActive(true);
        itemCost.SetActive(true);
        PriceText.text = price.ToString();
        empty = false;
    }

    public void DisableShop()
    {
        //GameObject.FindWithTag("Player").GetComponent<RopeSystem>().ResetRope();
        FindObjectOfType<AudioManager>().Play("ka-ching");
        displayItemPos.GetComponent<SpriteRenderer>().enabled = false;
        pushButtonIndicator.SetActive(false);
        itemCost.SetActive(false);
        empty = true;
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
                    if (!empty)
                    {
                        PlayerStats.Instance.PayGems(price);
                        Instantiate(soldObject, transform.position, Quaternion.identity);
                        DisableShop();
                    }
                }
                else
                {
                    // Not enough gems text
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }
#endif
}
