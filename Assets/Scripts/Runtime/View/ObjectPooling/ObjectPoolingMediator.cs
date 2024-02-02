using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingMediator : EventMediator
{
    [Inject]
    public ObjectPoolingView view { get; set; }
    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }

    public override void OnRegister()
    {
        Init();
    }

    private void Init()
    {
        objectPoolingModel.poolParent = view.objectPooling;
    }
}
