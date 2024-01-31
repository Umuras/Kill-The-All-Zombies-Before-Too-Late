using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveMediator : EventMediator
{
    [Inject]
    public PlayerMoveView view { get; set; }

    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }
    [Inject]
    public ILootBoxModel lootBoxModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        Init();
    }

    private void Init()
    {
        playerMoveModel.characterController = view.characterController;
        playerMoveModel.orientation = view.orientation;
        playerMoveModel.inputActions = view.inputActions;
    }

    private void FixedUpdate()
    {
        playerMoveModel.MovePlayer(view.orientation);
        lootBoxModel.isPlayerAround = playerMoveModel.ThrowRaycast(view.orientation);
    }

    private void Update()
    {
        playerMoveModel.InputPlayer();
        playerMoveModel.Gravity();
        playerMoveModel.GravityForce();
        playerMoveModel.GroundControl();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
