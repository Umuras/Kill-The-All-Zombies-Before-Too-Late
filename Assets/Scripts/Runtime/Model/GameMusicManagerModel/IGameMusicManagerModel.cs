using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameMusicManagerModel
{
    AudioSource audioSource { get; set; }
    AudioClip mainMenuClip { get; set; }
    AudioClip playButtonClip { get; set; }
    AudioClip trainingRoomClip { get; set; }
    AudioClip mainStageClip { get; set; }
    AudioClip waveClip { get; set; }
    AudioClip gameOverClip { get; set; }
    AudioClip mainStageInfoClip { get; set; }
    void InitializeAudioComponents(AudioSource audioSource, AudioClip mainMenuClip, AudioClip playButtonClip, AudioClip trainingRoomClip,
        AudioClip mainStageClip, AudioClip waveClip, AudioClip gameOverClip, AudioClip mainStageInfoClip);
}
