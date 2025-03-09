using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    public static MouseLookAround instance;

    [Header("Mouse variables")]
    [SerializeField] float sensitivity = 10f;
    [SerializeField] float fov = 60f;
    [SerializeField] float scopeFactor = 2f;
    public bool lookAllowed = false;

    [Header("Player Reference")]
    [SerializeField] Transform playerTrans;

    //internal variables
    private float xRotation = 0;
    private Camera cam;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAllowed)
        {
            ApplyLookAround();
        }

    }

    public void SetMouseLock(bool state = true)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ZoomIn(bool b)
    {
        if (b)
        {
            cam.fieldOfView = fov * 1 / scopeFactor;
        }
        else
        {
            cam.fieldOfView = fov;
        }
    }
    void ApplyLookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60, 60);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTrans.Rotate(Vector3.up, mouseX);
    }
}
