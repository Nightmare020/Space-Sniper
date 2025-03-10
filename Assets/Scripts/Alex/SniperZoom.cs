using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperZoom : MonoBehaviour
{
    public static SniperZoom Instance { get; private set; }

    public Camera mainCamera;

    // Desired FOV when zoomed in
    public float zoomedFOV = 30f;

    private float originalFOV;
    private bool isZoomedIn = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera != null)
        {
            originalFOV = mainCamera.fieldOfView;
        }
    }

    public void ToggleZoom()
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
