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
    [Inject]
    public IInputModel inputModel { get; set; }

    private const string PlayerControllerInputActionAsset = "PlayerController";

    public override void Execute()
    {
        //bundleModelde �al��an fonksiyon �al��t�ktan sonra geri d�n�� de�eri Then((playerController) i�indeki parametreye arg�man
        //olarak g�nderiliyor.
        bundleModel.AddressableLoadInputActionAsset(inputActionAssetAdress: PlayerControllerInputActionAsset).Then((playerController) =>
        {
            inputModel.playerController = playerController;
            uiPanelModel.OpenPanel(0, PanelKeys.MAINMENU);
        });
        
    }
   
}
