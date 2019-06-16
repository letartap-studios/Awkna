using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector3 movement_vector;

    private Vector2 viewPos;
    private Vector2 clampedWorldPos;

    public Vector3 offset;
    void FixedUpdate()
    {
        movement_vector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
             transform.Translate(movement_vector * speed, Space.Self);
        }

        viewPos = Camera.main.WorldToViewportPoint(transform.position + offset);
        clampedWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(Mathf.Clamp01(viewPos.x), Mathf.Clamp01(viewPos.y), 0));
        transform.position = clampedWorldPos;
    }
}
