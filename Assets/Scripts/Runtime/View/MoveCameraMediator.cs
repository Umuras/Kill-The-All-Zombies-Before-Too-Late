using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraMediator : EventMediator
{
   [Inject]
   public MoveCameraView view { get; set; }

    
    void Update()
    {
        transform.position = view.cameraPosition.position;
    }
}
