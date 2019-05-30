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

    private bool upRay;
    private bool downRay;
    private bool leftRay;
    private bool rightRay;

    public float radius;
    public LayerMask whatIsGround;

    public Vector2 upOffset;
    public Vector2 downOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        singleSprite = single[Random.Range(0, single.Length)];
        upLeftSprite = upLeft[Random.Range(0, upLeft.Length)];
        upSprite = up[Random.Range(0, up.Length)];
        upRightSprite = upRight[Random.Range(0, upRight.Length)];
        leftSprite = left[Random.Range(0, left.Length)];
        centreSprite = centre[Random.Range(0, centre.Length)];
        rightSprite = right[Random.Range(0, right.Length)];
        downLeftSprite = downLeft[Random.Range(0, downLeft.Length)];
        downSprite = down[Random.Range(0, down.Length)];
        downRightSprite = downRight[Random.Range(0, downRight.Length)];
        upDownLeftSprite = upDownLeft[Random.Range(0, upDownLeft.Length)];
        upDownSprite = upDown[Random.Range(0, upDown.Length)];
        upDownRightSprite = upDownRight[Random.Range(0, upDownRight.Length)];
        upLeftRightSprite = upLeftRight[Random.Range(0, upLeftRight.Length)];
        leftRightSprite = leftRight[Random.Range(0, leftRight.Length)];
        downLeftRightSprite = downLeftRight[Random.Range(0, downLeftRight.Length)];
    }

    private void Update()
    {
        ChangeSprite();

    }

    public void ChangeSprite()
    {
        upRay = Physics2D.OverlapCircle((Vector2)transform.position + upOffset, radius, whatIsGround);
        downRay = Physics2D.OverlapCircle((Vector2)transform.position + downOffset, radius, whatIsGround);
        leftRay = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, radius, whatIsGround);
        rightRay = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, radius, whatIsGround);

        if(!upRay && !downRay && !leftRay && !rightRay)     // single
        {
            spriteRenderer.sprite = singleSprite;
        }
        else if(!upRay && downRay && !leftRay && rightRay)  // Up Left 
        {
            spriteRenderer.sprite = upLeftSprite;
        }
        else if(!upRay && downRay && leftRay && rightRay)   // Up
        {
            spriteRenderer.sprite = upSprite;
        }
        else if(!upRay && downRay && leftRay && !rightRay)  // Up Right
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

    private void OnDrawGizmos()
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
