using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerView : EventView
{
    [HideInInspector]
    public int playerHealth = 100;
    public PostProcessVolume PostProcessVolume;
}
