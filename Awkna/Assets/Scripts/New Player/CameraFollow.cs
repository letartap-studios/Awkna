using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables
    public Transform target;                              // The refrence of what camera follows.
    public float smoothSpeed = 10f;                       // How much to smooth out the camera movement.
    public Vector3 offset;                                // Offset of the camera.

    public bool bounds;                                   // Whether the camera should have bounds or not, so it doesn't see out of the map.
    public Vector3 minCameraPos;                          // The minumum position at which the camera can go.
    public Vector3 maxCameraPos;                          // The maximum position at which the camera can go.
    #endregion

    private void LateUpdate()
    {
        if (target.GetComponent<PlayerController>() != null)
        {
            Vector3 desiredPosition = transform.position = target.position + offset;                                    // Follow the target
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // Smooth the camera movement
            transform.position = smoothedPosition;                                                                      // Set the position to the smoothed position

            if (bounds)                          // If the camera has bounds...
            {                                   //...set its maximum and minimum position.
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                    Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                    Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
            }
        }
    }

    #region Set the min and max camera positions
    public void SetMinCamPosition()                     // Set the minimum camera position in CameraFollowEditor.cs
    {
        minCameraPos = gameObject.transform.position;
    }

    public void SetMaxCamPosition()                     // Set the maximum camera position in CameraFollowEditor.cs
    {
        maxCameraPos = gameObject.transform.position;
    }
    #endregion
}
