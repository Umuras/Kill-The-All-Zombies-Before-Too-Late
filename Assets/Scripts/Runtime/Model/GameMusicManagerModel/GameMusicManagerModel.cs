using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManagerModel : IGameMusicManagerModel
{
    public AudioSource audioSource { get; set; }
    public AudioClip mainMenuClip { get; set; }
    public AudioClip playButtonClip { get; set; }
    public AudioClip trainingRoomClip { get; set; }
    public AudioClip mainStageClip { get; set; }
    public AudioClip waveClip { get; set; }
    public AudioClip gameOverClip { get; set; }
    public AudioClip mainStageInfoClip { get; set; }

    public void InitializeAudioComponents(AudioSource audioSource, AudioClip mainMenuClip, AudioClip playButtonClip, AudioClip trainingRoomClip,
        AudioClip mainStageClip, AudioClip waveClip, AudioClip gameOverClip, AudioClip mainStageInfoClip)
    {
        this.audioSource = audioSource;
        this.mainMenuClip = mainMenuClip;
        this.playButtonClip = playButtonClip;
        this.trainingRoomClip = trainingRoomClip;
        this.mainStageClip = mainStageClip;
        this.waveClip = waveClip;
        this.gameOverClip = gameOverClip;
        this.mainStageInfoClip = mainStageInfoClip;
    }
}
