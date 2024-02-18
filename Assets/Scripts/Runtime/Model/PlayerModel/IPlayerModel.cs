using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public interface IPlayerModel
{
    int playerHealth { get; set; }
    GameObject player { get; set; }
    PostProcessVolume PostProcessVolume { get; set; }
    void PlayerHitEffect();
}
