using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerCameraModel
{
    float xRotation { get; set; }
    float yRotation { get; set; }

    void CameraRotateAndOrientation(float sensX, float sensY, Transform transform, Transform orientation);
}
