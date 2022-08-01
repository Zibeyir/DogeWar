using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100;
    public Transform PlayerBody;
    float xRotation = 0;

    private void Start()
    {
        
     //   Cursor.lockState = CursorLockMode.Locked;
    }

    //private void Update()
    //{
    //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //    xRotation -= mouseY;
    //    xRotation = Mathf.Clamp(xRotation, -90, 90);

    //    transform.localRotation = Quaternion.Euler(0, 0, xRotation);
    //   // PlayerBody.Rotate(Vector3.forward * mouseY);

    //}
}
