using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer line;
    private DistanceJoint2D joint;
    private Vector3 targetPos;
    private RaycastHit2D hit;
    public float distance = 10f;
    public LayerMask whatIsGrappled;
    public float step = 0.2f;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }


    void Update()
    {

        if (joint.distance > 1f)
        {
            joint.distance -= step;
        }
        else
        {
            line.enabled = false;
            joint.enabled = false;
        }
        if (Input.GetButtonDown("Grapple"))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, whatIsGrappled);

            if(hit.collider!=null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }
        }

        if (Input.GetButtonDown("Grapple"))
        {
            line.SetPosition(0, transform.position);
        }

        if (Input.GetButtonUp("Grapple"))
        {
            joint.enabled = false;
            line.enabled = false;
        }
    }
}
