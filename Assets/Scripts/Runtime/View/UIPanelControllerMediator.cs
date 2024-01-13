using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelControllerMediator : EventMediator
{
    [Inject]
    public UIPanelControllerView view { get; set; }

    [Inject]
    public IUIPanelModel uiPanelModel { get; set; }

    public override void OnRegister()
    {
        Init();
    }

    private void Init()
    {
        uiPanelModel.layers = view.layers;
    }
}
