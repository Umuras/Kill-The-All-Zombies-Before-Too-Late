using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxMediator : EventMediator
{
    [Inject]
    public LootBoxView view { get; set; }
    [Inject]
    public ILootBoxModel lootBoxModel { get; set; }
    [Inject]
    public IInputModel inputModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
        Init();
    }

    private void Init()
    {
        lootBoxModel.closeOnExit = view.closeOnExit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(view.playerTag))
        {
            lootBoxModel.EnterPlayerLootBoxArea();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        lootBoxModel.ExitPlayerLootBoxArea();
    }

    private void Update()
    {
        if (lootBoxModel.isPlayerAround)
        {
            if (inputModel.playerController.FindAction(PlayerControllerInputActionKeys.Interaction.ToString()).triggered)
            {
                lootBoxModel.Open(view.animator);
            }
        }
        else
        {
            lootBoxModel.CloseBoxOperation(view.animator);
        }
    }


    public override void OnRemove()
    {
        base.OnRemove();
    }
}
