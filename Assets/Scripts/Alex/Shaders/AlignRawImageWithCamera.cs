using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignRawImageWithCamera : MonoBehaviour
{
    public Camera mainCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Synchronize position and rotation
            transform.position = mainCamera.transform.position + mainCamera.transform.forward * 0.1f;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
