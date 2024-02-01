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
        //bundleModelde çalýþan fonksiyon çalýþtýktan sonra geri dönüþ deðeri Then((playerController) içindeki parametreye argüman
        //olarak gönderiliyor.
        bundleModel.AddressableLoadInputActionAsset(inputActionAssetAdress: PlayerControllerInputActionAsset).Then((playerController) =>
        {
            inputModel.playerController = playerController;
            uiPanelModel.OpenPanel(0, PanelKeys.MAINMENU);
        });
        
    }
   
}
