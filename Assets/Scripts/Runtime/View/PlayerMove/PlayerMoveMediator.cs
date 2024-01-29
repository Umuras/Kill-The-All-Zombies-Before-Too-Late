using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMediator : EventMediator
{
    [Inject]
    public PlayerMoveView view { get; set; }

    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        Init();
    }

    private void Init()
    {
        view.rb.freezeRotation = true;
        playerMoveModel.orientation = view.orientation;
    }

    private void FixedUpdate()
    {
        playerMoveModel.MovePlayer(view.rb, view.orientation);
        playerMoveModel.SpeedControl(view.rb);
        playerMoveModel.HandleDrag(view.rb);
    }

    private void Update()
    {
        playerMoveModel.GroundControl(gameObject.transform);
        playerMoveModel.InputPlayer(view.rb, gameObject.transform); 
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
