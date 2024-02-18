using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveFinishPanelView : EventView
{
    public TextMeshProUGUI waveFinishText;
    public TextMeshProUGUI waveCounterText;
    [HideInInspector]
    public float waveDuration = 10f;
}
