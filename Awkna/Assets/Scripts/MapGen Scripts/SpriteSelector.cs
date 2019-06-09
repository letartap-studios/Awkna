using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    #region Arrays
    public Sprite[] upLeft;
    public Sprite[] up;
    public Sprite[] upRight;
    public Sprite[] upLeftRight;

    public Sprite[] left;
    public Sprite[] centre;
    public Sprite[] right;
    public Sprite[] leftRight;

    public Sprite[] downLeft;
    public Sprite[] down;
    public Sprite[] downRight;
    public Sprite[] downLeftRight;


    public Sprite[] upDownLeft;
    public Sprite[] upDown;
    public Sprite[] upDownRight;
    public Sprite[] single;
    #endregion
    #region Static sprites
    private Sprite upLeftSprite;
    private Sprite upSprite;
    private Sprite upRightSprite;
    private Sprite upLeftRightSprite;

    private Sprite leftSprite;
    private Sprite centreSprite;
    private Sprite rightSprite;
    private Sprite leftRightSprite;

    private Sprite downLeftSprite;
    private Sprite downSprite;
    private Sprite downRightSprite;
    private Sprite downLeftRightSprite;

    private Sprite upDownLeftSprite;
    private Sprite upDownSprite;
    private Sprite upDownRightSprite;
    private Sprite singleSprite;
    #endregion



    public float radius;
    public LayerMask whatIsGround;

    public Vector2 upOffset;
    public Vector2 downOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    private int randomId = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //this code assumes that all the arrays have the same length.
        //otherways it might crash

        randomId = Random.Range(0, single.Length);

        singleSprite = single[randomId];
        upLeftSprite = upLeft[randomId];
        upSprite = up[randomId];
        upRightSprite = upRight[randomId];
        leftSprite = left[randomId];
        centreSprite = centre[randomId];
        rightSprite = right[randomId];
        downLeftSprite = downLeft[randomId];
        downSprite = down[randomId];
        downRightSprite = downRight[randomId];
        upDownLeftSprite = upDownLeft[randomId];
        upDownSprite = upDown[randomId];
        upDownRightSprite = upDownRight[randomId];
        upLeftRightSprite = upLeftRight[randomId];
        leftRightSprite = leftRight[randomId];
        downLeftRightSprite = downLeftRight[randomId];

        ChangeSprite();
    }

    public void ChangeSprite()
    {

        bool upRay = Physics2D.OverlapCircle((Vector2)transform.position + upOffset, radius, whatIsGround);
        bool downRay = Physics2D.OverlapCircle((Vector2)transform.position + downOffset, radius, whatIsGround);
        bool leftRay = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, radius, whatIsGround);
        bool rightRay = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, radius, whatIsGround);

        if (!upRay && !downRay && !leftRay && !rightRay)     // single
        {
            spriteRenderer.sprite = singleSprite;
        }
        else if (!upRay && downRay && !leftRay && rightRay)  // Up Left 
        {
            spriteRenderer.sprite = upLeftSprite;
        }
        else if (!upRay && downRay && leftRay && rightRay)   // Up
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (!upRay && downRay && leftRay && !rightRay)  // Up Right
        {
            spriteRenderer.sprite = upRightSprite;
        }
        else if (upRay && downRay && !leftRay && rightRay)  // Left
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (upRay && downRay && leftRay && rightRay)   // Centre
        {
            spriteRenderer.sprite = centreSprite;
        }
        else if (upRay && downRay && leftRay && !rightRay)   // Right
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (upRay && !downRay && !leftRay && rightRay)   // Down Left
        {
            spriteRenderer.sprite = downLeftSprite;
        }
        else if (upRay && !downRay && leftRay && rightRay)   // Down
        {
            spriteRenderer.sprite = downSprite;
        }
        else if (upRay && !downRay && leftRay && !rightRay)   // Down Right
        {
            spriteRenderer.sprite = downRightSprite;
        }
        else if (!upRay && !downRay && !leftRay && rightRay)   // Up Down Left
        {
            spriteRenderer.sprite = upDownLeftSprite;
        }
        else if (!upRay && !downRay && leftRay && rightRay)   // Up Down
        {
            spriteRenderer.sprite = upDownSprite;
        }
        else if (!upRay && !downRay && leftRay && !rightRay)   // Up Down Right
        {
            spriteRenderer.sprite = upDownRightSprite;
        }
        else if (!upRay && downRay && !leftRay && !rightRay)   // Up Left Right
        {
            spriteRenderer.sprite = upLeftRightSprite;
        }
        else if (upRay && downRay && !leftRay && !rightRay)   // Left Right
        {
            spriteRenderer.sprite = leftRightSprite;
        }
        else if (upRay && !downRay && !leftRay && !rightRay)   // Down Left Right
        {
            spriteRenderer.sprite = downLeftRightSprite;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + upOffset, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + downOffset, radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, radius);
    }
}