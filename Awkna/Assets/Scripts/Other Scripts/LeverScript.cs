using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public Vector3 offset;
    public float range;

    public Sprite leverDownSprite;
    public Sprite leverUpSprite;

    SpriteRenderer spriteRenderer;

    private bool leverIsUp = true;

    public Animator anim;
    public Light led;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bool other = Physics2D.OverlapCircle(transform.position + offset, range, LayerMask.GetMask("Player"));
        if (other)
        {
            if (Input.GetButtonDown("Interact"))
            {
                GameObject[] electricty = GameObject.FindGameObjectsWithTag("Electricity");

                if (leverIsUp)
                {
                    FindObjectOfType<AudioManager>().Play("surge");

                    led.color = Color.red;
                    leverIsUp = false;
                    spriteRenderer.sprite = leverDownSprite;
                    for (int i = 0; i < electricty.Length; i++)
                    {
                        if (electricty[i].layer == 22)       // Light Bulb
                        {
                            //electricty[i].GetComponentInChildren<Light>().enabled = false;
                            electricty[i].transform.GetChild(0).gameObject.GetComponent<Light>().enabled = false;
                        }
                        else if (electricty[i].layer == 23)  // Laser
                        {
                            electricty[i].transform.GetComponent<Blink>().enabled = false;
                            electricty[i].transform.GetChild(0).gameObject.SetActive(false);
                        }
                        else if (electricty[i].layer == 24)  // Electrical Door
                        {
                            electricty[i].GetComponent<DoorScript>().OpenDoor();
                        }
                        else if (electricty[i].layer == 27) // Shop
                        {
                            electricty[i].GetComponent<ShopItem>().DisableShop();
                        }
                    }
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("power_on");
                    led.color = Color.green;
                    leverIsUp = true;
                    spriteRenderer.sprite = leverUpSprite;
                    
                    for (int i = 0; i < electricty.Length; i++)
                    {
                        if (electricty[i].layer == 22)       // Light Bulb
                        {
                            electricty[i].transform.GetChild(0).gameObject.GetComponent<Light>().enabled = true;
                        }
                        else if (electricty[i].layer == 23)  // Laser
                        {
                            electricty[i].transform.GetComponent<Blink>().enabled = true;
                            electricty[i].transform.GetChild(0).gameObject.SetActive(true);
                        }
                        else if (electricty[i].layer == 24)  // Electrical Door
                        {
                            electricty[i].GetComponent<DoorScript>().CloseDoor();
                        }
                        else if (electricty[i].layer == 27) // Shop
                        {
                            electricty[i].GetComponent<ShopItem>().EnableShop();
                        }
                    }
                }
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
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, range);
    }
#endif
}
