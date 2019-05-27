using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopeSystem : MonoBehaviour
{
    #region Varibles
    public GameObject ropeHingeAnchor;
    private DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    private PlayerController playerController;

    private LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    public float ropeMaxCastDistance = 10f;
    public float step = 0.2f;

    private List<Vector2> ropePositions = new List<Vector2>();

    private bool distanceSet;

    [HideInInspector]
    public float waitTime;
    public float startWaitTime;

    public float climbSpeed = 3f;      // Set the speed at which the player can go up and down the rope.
    private bool isColliding;          // Flag to determine whether or not the rope's distance joint distance property can be increased or decreased.     

    private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();

    private bool launch = true;
    #endregion
    private void Awake()
    {
        ropeJoint = GetComponent<DistanceJoint2D>();
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        ropeRenderer = GetComponent<LineRenderer>();
        waitTime = startWaitTime;
    }

    private void LateUpdate()
    {
        

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 facingDirection = new Vector3(worldMousePosition.x, worldMousePosition.y, transform.position.z) - transform.position;
        float aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle += (2 * Mathf.PI);
        }

        Vector3 aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        playerPosition = transform.position;

        if (!ropeAttached)
        {
            SetCrosshairPosition(aimAngle);
            playerController.isSwinging = false;
        }
        else
        {
            crosshairSprite.enabled = false;
            if (ropePositions.Count > 0)
            {
                var lastRopePoint = ropePositions.Last();
                var playerToCurrentNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, ropeLayerMask);

                if (playerToCurrentNextHit)
                {
                    var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
                    if (colliderWithVertices != null)
                    {
                        var closestPointToHit = GetClosestColliderPointFromRaycastHit(playerToCurrentNextHit, colliderWithVertices);

                        if (wrapPointsLookup.ContainsKey(closestPointToHit))
                        {
                            ResetRope();
                            return;
                        }

                        ropePositions.Add(closestPointToHit);
                        wrapPointsLookup.Add(closestPointToHit, 0);
                        distanceSet = false;
                    }
                }
            }

            playerController.isSwinging = true;
            playerController.ropeHook = ropePositions.Last();

        }

        HandleInput(aimDirection);

        UpdateRopePositions();

        HandleRopeLength();

        HandleRopeUnwrap();
    }

    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        float x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        float y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        Vector3 crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetButtonDown("Grapple") && waitTime == startWaitTime)
        {
            if (ropeAttached)
            {
                ResetRope();
                return;
            }

            ropeRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);

            if (hit.collider != null)
            {
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    FindObjectOfType<AudioManager>().Play("hook"); //play sound
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                    //transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
                }

                waitTime = 0;
            }
            else
            {
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }
        if (Input.GetButtonDown("Jump") && playerController.isSwinging)
        {
            ResetRope();
        }

        if (waitTime < startWaitTime)
        {
            waitTime += Time.deltaTime;
            if (waitTime > startWaitTime)
            {
                waitTime = startWaitTime;
            }
        }
    }

    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        playerController.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        launch = true;
        ropeHingeAnchorSprite.enabled = false;
        wrapPointsLookup.Clear();
    }

    private void UpdateRopePositions()
    {
        // Return out of this method if the rope isn't actually attached.
        if (!ropeAttached)
        {
            return;
        }

        /* Set the rope's line renderer vertex count (positions) to whatever number
         * of positions are stored in ropePositions, plus 1 more (for the player's position).
         */
        ropeRenderer.positionCount = ropePositions.Count + 1;


        /* Loop backwards through the ropePositions list, and for every position (except the last position),
         * set the line renderer vertex position to the Vector2 position stored at the 
         * current index being looped through in ropePositions.
         */
        for (int i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);
                /* Set the rope anchor to the second-to-last rope position where the current hinge/anchor
                 * should be, or if there is only one rope position, then set that one to be the anchor point.
                 * This configures the ropeJoint distance to the distance between the player and
                 * the current rope position being looped over.
                 */
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    Vector2 ropePosition = ropePositions[ropePositions.Count - 1];
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
                /* Set the rope anchor to the second-to-last rope position where the current hinge/anchor
                 should be, or if there is only one rope position, then set that one to be the anchor point.
                 This configures the ropeJoint distance to the distance between the 
                 player and the current rope position being looped over.
                */
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    Vector2 ropePosition = ropePositions.Last();
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
                // Handle setting the rope's last vertex position to the player's current position.
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }

    private void HandleRopeLength()
    {
        if (ropeJoint.distance > (ropeMaxCastDistance / 2) && launch)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;

            if (ropeJoint.distance <= (ropeMaxCastDistance / 2))
            {
                launch = false;
            }
        }
        if (Input.GetAxisRaw("Vertical") > 0f && ropeAttached && !isColliding)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f && ropeAttached && !playerController.isGrounded)
        {
            if (ropeJoint.distance >= ropeMaxCastDistance)
            {
                return;
            }
            else
            {
                ropeJoint.distance += Time.deltaTime * climbSpeed;
            }
        }
    }

    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
            position => polyCollider.transform.TransformPoint(position));

        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }

    private void HandleRopeUnwrap()
    {
        if (ropePositions.Count <= 1)
        {
            return;
        }

        var anchorIndex = ropePositions.Count - 2;
        var hingeIndex = ropePositions.Count - 1;
        var anchorPosition = ropePositions[anchorIndex];
        var hingePosition = ropePositions[hingeIndex];
        var hingeDir = hingePosition - anchorPosition;
        var hingeAngle = Vector2.Angle(anchorPosition, hingeDir);
        var playerDir = playerPosition - anchorPosition;
        var playerAngle = Vector2.Angle(anchorPosition, playerDir);

        if (!wrapPointsLookup.ContainsKey(hingePosition))
        {
            Debug.LogError("We were not tracking hingePosition (" + hingePosition + ") in the look up dictionary.");
            return;
        }

        if (playerAngle < hingeAngle)
        {
            if (wrapPointsLookup[hingePosition] == 1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }
            wrapPointsLookup[hingePosition] = -1;
        }
        else
        {
            if (wrapPointsLookup[hingePosition] == -1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }
            wrapPointsLookup[hingePosition] = 1;
        }

    }

    private void UnwrapRopePosition(int anchorIndex, int hingeIndex)
    {
        var newAnchorPosition = ropePositions[anchorIndex];
        wrapPointsLookup.Remove(ropePositions[hingeIndex]);
        ropePositions.RemoveAt(hingeIndex);

        ropeHingeAnchorRb.transform.position = newAnchorPosition;
        distanceSet = false;

        // Set new rope distance joint distance for anchor position if not yet set.
        if (distanceSet)
        {
            return;
        }
        ropeJoint.distance = Vector2.Distance(transform.position, newAnchorPosition);
        distanceSet = true;
    }

    public void AddRope(float x)
    {
        ropeMaxCastDistance += x;
    }
}
