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
    public AudioSource playerAudioSource;
    public AudioClip playerEnterClip;
    public AudioClip playerHitClip;
    public AudioClip playerDeathClip;
}
