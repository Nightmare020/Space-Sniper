using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMove : MonoBehaviour
{
    public static SniperMove Instance { get; private set; }

    public Camera sniperView;
    public float rotationSpeed = 100f;
    public Vector3 minRotation;
    public Vector3 maxRotation;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool canMove = true;
    [NonSerialized] public bool isInGame = false;

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

    // Update is called once per frame
    void Update()
    {
        if (canMove && Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            rotationX += mouseX;
            rotationY -= mouseY;

            // Clamp the position to the specified limits
            rotationY = Mathf.Clamp(rotationY, minRotation.x, maxRotation.x);
            rotationX = Mathf.Clamp(rotationX, minRotation.y, maxRotation.y);

            sniperView.transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EnableMovement();
    }
}
