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


    public override void OnRegister()
    {
        view.dispatcher.AddListener(MainMenuEvent.Play, OnPlay);
        view.dispatcher.AddListener(MainMenuEvent.Exit, OnExit);
    }

    private void OnPlay()
    {
        bundleModel.AddressableInstantiate(GameAreaKeys.TRAININGLEVEL, gameObject.transform.parent.transform.parent.transform.parent.transform.parent).Then(() =>
        {
            uIPanelModel.ClosePanel(0);
            uIPanelModel.OpenPanel(1, PanelKeys.PLAYERANDWEAPONUI);
        });
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
