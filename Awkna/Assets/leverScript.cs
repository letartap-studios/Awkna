using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    public Vector3 offset;
    public float range;

    public Sprite leverDownSprite;

    // Update is called once per frame
    void Update()
    {

        bool other = Physics2D.OverlapCircle(transform.position + offset, range, LayerMask.GetMask("Player"));
        if (other)
        {
            if (Input.GetButtonDown("Interact"))
            {
                SpriteRenderer spriteRenderer;
                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = leverDownSprite;

                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

            }
            else
            {
                return;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, range);
    }
}
