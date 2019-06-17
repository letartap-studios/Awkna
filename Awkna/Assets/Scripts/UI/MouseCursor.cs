using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite full;
    public Sprite half;
    public Sprite empty;

    public GameObject pause;
    public GameObject gameOver;



    private void Start()
    {
        Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(pause.activeSelf || gameOver.activeSelf)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;

            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;

            if (PlayerStats.Instance.UsesUsed == 2)
            {
                spriteRenderer.sprite = full;
            }
            else if (PlayerStats.Instance.UsesUsed == 1)
            {
                spriteRenderer.sprite = half;
            }
            else if (PlayerStats.Instance.UsesUsed == 0)
            {
                spriteRenderer.sprite = empty;
            }
        }
    }
}
