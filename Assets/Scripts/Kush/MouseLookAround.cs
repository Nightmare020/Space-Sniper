using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    public static MouseLookAround instance;

    [Header("Mouse Look variables")]
    [SerializeField] float sensitivity = 10f;
    [SerializeField] bool lookAllowed = false;

    [Header("Player Reference")]
    [SerializeField] Transform playerTrans;

    //internal variables
    private float xRotation = 0;

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
        //Remove later (debug only)
        lookAllowed = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAllowed)
        {
            ApplyLookAround();
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
