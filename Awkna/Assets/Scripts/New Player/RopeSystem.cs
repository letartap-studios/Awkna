using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeSystem : MonoBehaviour
{
    // 1
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public PlayerController playerMovement;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;

    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();

    private bool distanceSet;



    void Awake()
    {
        // 2
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 3
        var worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        // 4
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        // 5
        playerPosition = transform.position;

        // 6
        if (!ropeAttached)
        {
            SetCrosshairPosition(aimAngle);
        }
        else
        {
            crosshairSprite.enabled = false;
        }

        HandleInput(aimDirection);

        UpdateRopePositions();


    }

    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }

    // 1
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetMouseButton(0))
        {
            // 2
            if (ropeAttached) return;
            ropeRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);

            // 3
            if (hit.collider != null)
            {
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    // 4
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
                }
            }
            // 5
            else
            {
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            ResetRope();
        }
    }

    // 6
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
    }

    private void UpdateRopePositions()
    {
        // 1
        if (!ropeAttached)
        {
            return;
        }

        // 2
        ropeRenderer.positionCount = ropePositions.Count + 1;

        // 3
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);

                // 4
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                // 5
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // 6
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }

}


//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class RopeSystem : MonoBehaviour
//{
//    public GameObject ropeHingeAnchor;
//    private DistanceJoint2D ropeJoint;
//    public Transform crosshair;
//    public SpriteRenderer crosshairSprite;
//    private bool ropeAttached;
//    private Vector2 playerPosition;
//    private Rigidbody2D ropeHingeAnchorRb;
//    private SpriteRenderer ropeHingeAnchorSprite;
//    private PlayerController playerController;

//    private LineRenderer ropeRenderer;
//    public LayerMask ropeLayerMask;
//    public float ropeMaxCastDistance = 10f;
//    public float step = 0.2f;

//    private List<Vector2> ropePositions = new List<Vector2>();

//    private bool distanceSet;

//    [HideInInspector]
//    public float waitTime;
//    public float startWaitTime;

//    private void Awake()
//    {
//        ropeJoint = GetComponent<DistanceJoint2D>();
//        ropeJoint.enabled = false;
//        playerPosition = transform.position;
//        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
//        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
//        playerController = GetComponent<PlayerController>();
//        ropeRenderer = GetComponent<LineRenderer>();
//        waitTime = startWaitTime;
//    }
//    private void FixedUpdate()
//    {
//        //if(ropeJoint.distance > 1f)
//        //{
//        //    ropeJoint.distance -= step;
//        //}
//        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
//        Vector3 facingDirection = new Vector3(worldMousePosition.x, worldMousePosition.y, transform.position.z) - transform.position;
//        float aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
//        if (aimAngle < 0f)
//        {
//            aimAngle += (2 * Mathf.PI);
//        }

//        Vector3 aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

//        playerPosition = transform.position;

//        if (!ropeAttached)
//        {
//            SetCrosshairPosition(aimAngle);
//        }
//        else
//        {
//            crosshairSprite.enabled = false;
//            playerController.isGrappled = true;
//        }

//        HandleInput(aimDirection);

//        UpdateRopePositions();

//    }

//    private void SetCrosshairPosition(float aimAngle)
//    {
//        if (!crosshairSprite.enabled)
//        {
//            crosshairSprite.enabled = true;
//        }

//        float x = transform.position.x + 1f * Mathf.Cos(aimAngle);
//        float y = transform.position.y + 1f * Mathf.Sin(aimAngle);

//        Vector3 crossHairPosition = new Vector3(x, y, 0);
//        crosshair.transform.position = crossHairPosition;
//    }

//    private void HandleInput(Vector2 aimDirection)
//    {
//        if (Input.GetButtonDown("Grapple") &&  waitTime == startWaitTime)
//        {
//            if (ropeAttached)
//            {
//                return;
//            }

//            ropeRenderer.enabled = true;

//            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);

//            if (hit.collider != null)
//            {
//                ropeAttached = true;
//                if (!ropePositions.Contains(hit.point))
//                {
//                    FindObjectOfType<AudioManager>().Play("hook");//play sound
//                    // Jump slightly to distance the player a little from the ground after grappling to something.
//                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
//                    ropePositions.Add(hit.point);
//                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
//                    ropeJoint.enabled = true;
//                    ropeHingeAnchorSprite.enabled = true;
//                }
//                waitTime = 0;
//            }
//            else
//            {
//                ropeRenderer.enabled = false;
//                ropeAttached = false;
//                ropeJoint.enabled = false;
//            }            
//        }
//        if (Input.GetButtonDown("Jump") && playerController.isGrappled) 
//        {
//            ResetRope();
//        }

//        if (waitTime < startWaitTime)
//        {
//            waitTime += Time.deltaTime;
//            if (waitTime > startWaitTime) 
//            {
//                waitTime = startWaitTime;
//            }
//        }
//    }

//    private void ResetRope()
//    {
//        ropeJoint.enabled = false;
//        ropeAttached = false;
//        playerController.isSwinging = false;
//        ropeRenderer.positionCount = 2;
//        ropeRenderer.SetPosition(0, transform.position);
//        ropeRenderer.SetPosition(1, transform.position);
//        ropePositions.Clear();
//        ropeHingeAnchorSprite.enabled = false;
//        playerController.isGrappled = false;
//    }

//    private void UpdateRopePositions()
//    {
//        if (!ropeAttached)
//        {
//            return;
//        }

//        ropeRenderer.positionCount = ropePositions.Count + 1;

//        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
//        {
//            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
//            {
//                ropeRenderer.SetPosition(i, ropePositions[i]);

//                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
//                {
//                    var ropePosition = ropePositions[ropePositions.Count - 1];
//                    if (ropePositions.Count == 1)
//                    {
//                        ropeHingeAnchorRb.transform.position = ropePosition;
//                        if (!distanceSet)
//                        {
//                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
//                            distanceSet = true;
//                        }
//                    }
//                    else
//                    {
//                        ropeHingeAnchorRb.transform.position = ropePosition;
//                        if (!distanceSet)
//                        {
//                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
//                            distanceSet = true;
//                        }
//                    }
//                }
//                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
//                {
//                    var ropePosition = ropePositions.Last();
//                    ropeHingeAnchorRb.transform.position = ropePosition;
//                    if (!distanceSet)
//                    {
//                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
//                        distanceSet = true;
//                    }
//                }
//            }
//            else
//            {
//                ropeRenderer.SetPosition(i, transform.position);
//            }
//        }
//    }

//    public void AddRope(float x)
//    {
//        ropeMaxCastDistance += x;
//    }
//}
