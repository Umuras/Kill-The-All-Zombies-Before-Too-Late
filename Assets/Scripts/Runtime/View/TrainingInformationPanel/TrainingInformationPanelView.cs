using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingInformationPanelView : EventView
{
    public Button PassGameButton;

    public void OnClosePanelButton()
    {
        dispatcher.Dispatch(TrainingInformationPanelEvent.ClosePanel);
    }
}
