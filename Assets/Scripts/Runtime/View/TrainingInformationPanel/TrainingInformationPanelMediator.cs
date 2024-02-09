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

    public override void OnRegister()
    {
        view.dispatcher.AddListener(TrainingInformationPanelEvent.ClosePanel, OnClosePanel);
    }

    private void OnClosePanel()
    {
        view.PassGameButton.interactable = false;
        bundleModel.AddressableInstantiate(GameAreaKeys.TRAININGLEVEL, gameObject.transform.parent.transform.parent.transform.parent.transform.parent).Then(() =>
        {
            uIPanelModel.OpenPanel(1, PanelKeys.PLAYERANDWEAPONUI).Then(() =>
            {
                uIPanelModel.ClosePanel(2);
            });
        });
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(TrainingInformationPanelEvent.ClosePanel, OnClosePanel);
    }
}