using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMediator : EventMediator
{
    [Inject]
    public PlayerCameraView view { get; set; }

    [Inject]
    public IPlayerCameraModel playerCameraModel { get; set; }

    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }

    private void Update()
    {
        playerCameraModel.CameraRotateAndOrientation(view.sensX, view.sensY, gameObject.transform, playerMoveModel.orientation);
    }

}
