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


    public override void OnRegister()
    {
        view.dispatcher.AddListener(MainMenuEvent.Play, OnPlay);
        view.dispatcher.AddListener(MainMenuEvent.Exit, OnExit);
    }

    private void OnPlay(IEvent payload)
    {
        var s = payload.data as string;
        var x = payload.data;
        Debug.Log(s + " " + x);
    }

    private void OnExit()
    {
        Application.Quit();
        Debug.Log("Exit Program.");
    }

    public override void OnRemove()
    {
        view.dispatcher.RemoveListener(MainMenuEvent.Play, OnPlay);
        view.dispatcher.RemoveListener(MainMenuEvent.Exit, OnExit);
    }
}
