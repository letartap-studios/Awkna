using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public Vector3 offset;
    public float range;

    public Sprite leverDownSprite;

    SpriteRenderer spriteRenderer;

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
                spriteRenderer.sprite = leverDownSprite;

                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                GameObject[] electricty = GameObject.FindGameObjectsWithTag("Electricity");
                for (int i = 0; i < electricty.Length; i++)
                {
                    if(electricty[i].layer == 22)       // Light Bulb
                    {
                        electricty[i].GetComponentInChildren<Light>().enabled = false;
                    }
                    else if(electricty[i].layer == 23)  // Laser
                    {
                        electricty[i].transform.GetComponent<Blink>().enabled = false;
                        electricty[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if(electricty[i].layer == 24)  // Electrical Door
                    {
                        electricty[i].GetComponent<DoorScript>().OpenDoor();
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, range);
    }
}
