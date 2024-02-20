using DG.Tweening;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortalEvent
{
    OpenPortal
}

public class PortalMediator : EventMediator
{
    [Inject]
    public PortalView view { get; set; }
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }
    [Inject]
    public IGameMusicManagerModel gameMusicManagerModel { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(PortalEvent.OpenPortal, OnOpenPortal);
        Init();
    }

    private void Init()
    {
        gameObject.SetActive(false);
    }

    private void OnOpenPortal()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            uIPanelModel.OpenPanel(2, PanelKeys.MAINSTAGEINFOPANEL).Then(() =>
            {
                PlayMainStageInfoMusic();
                uIPanelModel.isOpenPanel = true;
                DOTween.KillAll();
                Destroy(transform.parent.gameObject);
            });
        }
    }

    private void PlayMainStageInfoMusic()
    {
        gameMusicManagerModel.audioSource.clip = gameMusicManagerModel.mainStageInfoClip;
        gameMusicManagerModel.audioSource.loop = false;
        gameMusicManagerModel.audioSource.Play();
    }


    public override void OnRemove()
    {
        dispatcher.RemoveListener(PortalEvent.OpenPortal, OnOpenPortal);
    }
}
