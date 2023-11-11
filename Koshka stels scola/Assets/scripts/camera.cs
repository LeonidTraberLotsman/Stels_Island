using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    float xRotation = 0;
    float yRotation = 0;
    public float sens = 1;

    public Transform camCenter;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens;
        float mouseY = Input.GetAxis("Mouse Y") * sens;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40, 40);
        yRotation -= mouseX;
        //yRotation = Mathf.Clamp(xRotation, -40, 40);

        camCenter.localRotation = Quaternion.Euler(xRotation, -yRotation,0 );
        //playerBody.Rotate(Vector3.up * mouseX);
    }
}
