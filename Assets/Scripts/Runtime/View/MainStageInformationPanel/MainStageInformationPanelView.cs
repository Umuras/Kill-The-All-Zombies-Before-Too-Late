using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainStageInformationPanelView : EventView
{
    public Button readyButton;
    public void OnEnterMainStage()
    {
        dispatcher.Dispatch(MainStageInformationPanelEvent.EnterMainStage);
    }
}
