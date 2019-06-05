using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour
{
    public int vertexCount = 40;
    public float lineWidth = 0.2f;
    private float radius;
    public bool circleFillscreen;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        radius = PlayerStats.Instance.RopeMaxDistance;

        
    }

    private void Update()
    {
        SetupCircle();
    }

    private void SetupCircle()
    {
        lineRenderer.widthMultiplier = lineWidth;

        if (circleFillscreen)
        {
            radius = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
                Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - lineWidth;
        }

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        lineRenderer.positionCount = vertexCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, transform.position + pos);
            theta += deltaTheta;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        Vector3 oldPos = transform.position;
        for (int i = 0; i <= vertexCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, transform.position + pos);
            oldPos = transform.position + pos;

            theta += deltaTheta;
        }
    }
#endif
}
