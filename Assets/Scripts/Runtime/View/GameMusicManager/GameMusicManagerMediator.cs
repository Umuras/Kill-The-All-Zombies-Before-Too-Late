using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManagerMediator : EventMediator
{
    [Inject]
    public GameMusicManagerView view { get; set; }
    [Inject]
    public IGameMusicManagerModel gameMusicManagerModel { get; set; }

    public override void OnRegister()
    {
        gameMusicManagerModel.InitializeAudioComponents(view.audioSource, view.mainMenuClip,view.playButtonClip, 
            view.trainingRoomClip, view.mainStageClip, view.waveClip, view.gameOverClip, view.mainStageInfoClip);
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
