using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediator : EventMediator
{
    [Inject]
    public PlayerView view { get; set; }
    [Inject]
    public IPlayerModel playerModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
        Init();
    }

    private void Init()
    {
        playerModel.playerHealth = view.playerHealth;
        playerModel.player = gameObject;
        playerModel.PostProcessVolume = view.PostProcessVolume;
        playerModel.playerAudioSource = view.playerAudioSource;
        playerModel.playerHitClip = view.playerHitClip;
        playerModel.playerEnterClip = view.playerEnterClip;
        playerModel.playerDeathClip = view.playerDeathClip;
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
