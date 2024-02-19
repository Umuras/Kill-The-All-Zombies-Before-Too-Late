using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public interface IPlayerModel
{
    int playerHealth { get; set; }
    GameObject player { get; set; }
    PostProcessVolume PostProcessVolume { get; set; }
    AudioSource playerAudioSource { get; set; }
    AudioClip playerEnterClip { get; set; }
    AudioClip playerHitClip { get; set; }
    AudioClip playerDeathClip { get; set; }
    void PlayerHitEffect();
}
