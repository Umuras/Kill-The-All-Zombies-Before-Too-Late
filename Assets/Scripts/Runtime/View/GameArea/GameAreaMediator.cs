using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaMediator : EventMediator
{
    [Inject]
    public GameAreaView view { get; set; }

    [Inject]
    public IGameAreaModel gameAreaModel { get; set; }

    public override void OnRegister()
    {
        Init();
    }

    private void Init()
    {
        gameAreaModel.GameAreaTransform = view.GameAreaTransform;
    }

    public override void OnRemove()
    {

    }
}
