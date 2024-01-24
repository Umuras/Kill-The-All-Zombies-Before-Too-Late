using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraView : EventView
{
    [SerializeField]
    public float sensX;
    public float sensY;

    [SerializeField]
    public Transform orientation;

    [SerializeField]
    public float xRotation;
    public float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
