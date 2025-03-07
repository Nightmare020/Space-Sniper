using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncBlurredCamera : MonoBehaviour
{
    // Main camera
    public Camera mainCamera;

    // Blurred camera
    private Camera blurredCamera;

    // Start is called before the first frame update
    void Start()
    {
        blurredCamera = GetComponent<Camera>();

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not set!");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Synchronize position and rotation
            blurredCamera.transform.position = mainCamera.transform.position;
            blurredCamera.transform.rotation = mainCamera.transform.rotation;

            // Make sure proyection values are the same
            blurredCamera.fieldOfView = mainCamera.fieldOfView;
            blurredCamera.nearClipPlane = mainCamera.nearClipPlane;
            blurredCamera.farClipPlane = mainCamera.farClipPlane;
        }
    }
}
