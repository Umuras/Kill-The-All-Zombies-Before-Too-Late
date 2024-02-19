using DG.Tweening;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrainingInformationPanelEvent
{
    ClosePanel
}

public class TrainingInformationPanelMediator : EventMediator
{
    [Inject]
    public TrainingInformationPanelView view { get; set; }
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }
    [Inject]
    public IBundleModel bundleModel { get; set; }
    [Inject]
    public IGameAreaModel gameAreaModel { get; set; }
    [Inject]
    public IGameMusicManagerModel gameMusicManagerModel { get; set; }

    public override void OnRegister()
    {
        view.dispatcher.AddListener(TrainingInformationPanelEvent.ClosePanel, OnClosePanel);
    }

    public void OnClosePanel()
    {
        view.PassGameButton.interactable = false;
        bundleModel.AddressableInstantiate(GameAreaKeys.TRAININGLEVEL, gameAreaModel.GameAreaTransform).Then(() =>
        {
            bundleModel.AddressableInstantiate("Player", gameAreaModel.GameAreaTransform).Then(() =>
            {
                uIPanelModel.OpenPanel(1, PanelKeys.PLAYERANDWEAPONUI).Then(() =>
                {
                    InitTrainingRoomMusic();
                    uIPanelModel.ClosePanel(2);
                });
            });
        });
    }

    private void InitTrainingRoomMusic()
    {
        gameMusicManagerModel.audioSource.clip = gameMusicManagerModel.trainingRoomClip;
        gameMusicManagerModel.audioSource.loop = true;
        gameMusicManagerModel.audioSource.Play();
    }

    public override void OnRemove()
    {
        view.dispatcher.RemoveListener(TrainingInformationPanelEvent.ClosePanel, OnClosePanel);
    }
}
