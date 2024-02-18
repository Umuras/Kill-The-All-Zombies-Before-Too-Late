using DG.Tweening;
using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameCommand : EventCommand
{
    [Inject]
    public IGameAreaModel gameAreaModel { get; set; }
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }

    public override void Execute()
    {
        DOTween.Clear();
        uIPanelModel.isOpenPanel = false;
        foreach (Transform item in gameAreaModel.GameAreaTransform)
        {
            Object.Destroy(item.gameObject);
        }
        uIPanelModel.AllClosePanels();
        SceneManager.LoadScene(1);
    }
}
