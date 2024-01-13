using RSG;
using strange.extensions.command.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameInitializeCommand : EventCommand
{
    [Inject]
    public IBundleModel bundleModel { get; set; }

    [Inject]
    public IUIPanelModel uiPanelModel { get; set; }

    public override void Execute()
    {
        uiPanelModel.OpenPanel(0, "MainMenu");
    }

    
}
