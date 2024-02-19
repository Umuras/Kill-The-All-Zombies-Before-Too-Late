using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuEvent
{
    Play,
    Exit
}


public class MainMenuMediator : EventMediator
{
    [Inject]
    public MainMenuView view { get; set; }

    [Inject]
    public IBundleModel bundleModel { get; set; }
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }
    [Inject]
    public IGameMusicManagerModel gameMusicManagerModel { get; set; }


    public override void OnRegister()
    {
        view.dispatcher.AddListener(MainMenuEvent.Play, OnPlay);
        view.dispatcher.AddListener(MainMenuEvent.Exit, OnExit);
        Cursor.lockState = CursorLockMode.None;
        InitMainMenuMusic();
    }

    private void InitMainMenuMusic()
    {
        gameMusicManagerModel.audioSource.clip = gameMusicManagerModel.mainMenuClip;
        gameMusicManagerModel.audioSource.loop = true;
        gameMusicManagerModel.audioSource.Play();
    }

    private void InitPlayButtonMusic()
    {
        gameMusicManagerModel.audioSource.clip = gameMusicManagerModel.playButtonClip;
        gameMusicManagerModel.audioSource.loop = false;
        gameMusicManagerModel.audioSource.Play();
    }

    private void OnPlay()
    {
        InitPlayButtonMusic();
        uIPanelModel.ClosePanel(0);
        uIPanelModel.OpenPanel(2, PanelKeys.TRAININGINFOPANEL);
    }

    private void OnExit()
    {
        Application.Quit();
    }

    public override void OnRemove()
    {
        view.dispatcher.RemoveListener(MainMenuEvent.Play, OnPlay);
        view.dispatcher.RemoveListener(MainMenuEvent.Exit, OnExit);
    }
}
