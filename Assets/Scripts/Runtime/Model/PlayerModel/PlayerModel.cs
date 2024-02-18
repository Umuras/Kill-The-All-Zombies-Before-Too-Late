using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerModel : IPlayerModel
{
    public int playerHealth { get; set; }
    public GameObject player { get; set; }

    public PostProcessVolume PostProcessVolume { get; set; }

    private Vignette _vignette;
    private int _coolDownTime = 500;

    public void PlayerHitEffect()
    {
        PostProcessVolume.profile.TryGetSettings<Vignette>(out _vignette);

        if (_vignette)
        {
            _vignette.intensity.Override(Mathf.Lerp(0, 0.8f, 0.5f));
            PlayerHitEffectCoolDownTimer(_vignette);
        }
    }

    private async void PlayerHitEffectCoolDownTimer(Vignette vignette)
    {
        await CoolDownTimer(vignette);
    }

    private async Task CoolDownTimer(Vignette vignette)
    {
        await Task.Delay(_coolDownTime);
        vignette.intensity.Override(Mathf.Lerp(0.8f, 0, 1));
    }
}
