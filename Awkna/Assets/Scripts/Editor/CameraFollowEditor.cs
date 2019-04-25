using UnityEditor;
using UnityEngine;

// This script adds buttons in the editor to set the min and max camera positions.


[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraFollow cameraFollow = (CameraFollow)target;

        if (GUILayout.Button("Set minimum camera position")) // If the button is pressed...
        {
            cameraFollow.SetMinCamPosition();                // ...set the min camera position.
        }

        if (GUILayout.Button("Set maximum camera position")) // If the button is pressed...
        {
            cameraFollow.SetMaxCamPosition();                // ...set the max camera position.
        }

    }
}
