using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerCameraModel
{
    float sensX { get; set; }
    float sensY { get; set; }
    Transform orientation { get; set; }
    float xRotation { get; set; }
    float yRotation { get; set; }

    void CameraRotateAndOrientation();
}
