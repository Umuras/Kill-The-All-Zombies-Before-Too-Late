using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraModel : IPlayerCameraModel
{
    public float xRotation { get; set; }
    public float yRotation { get; set; }

    public void CameraRotateAndOrientation(float sensX, float sensY, Transform transform, Transform orientation)
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
