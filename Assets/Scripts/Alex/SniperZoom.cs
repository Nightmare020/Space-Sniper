using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperZoom : MonoBehaviour
{
    public Camera mainCamera;

    // Desired FOV when zoomed in
    public float zoomedFOV = 30f;

    private float originalFOV;
    private bool isZoomedIn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera != null)
        {
            originalFOV = mainCamera.fieldOfView;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleZoom();
        }
    }

    private void ToggleZoom()
    {
        if (mainCamera != null)
        {
            if (isZoomedIn)
            {
                mainCamera.fieldOfView = originalFOV;
            }
            else
            {
                mainCamera.fieldOfView = zoomedFOV;
            }

            isZoomedIn = !isZoomedIn;
        }
    }
}
