using DG.Tweening;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameOverPanelEvent
{
    TryAgain,
    RestartGame
}

public class GameOverPanelMediator : EventMediator
{
    [Inject]
    public GameOverPanelView view { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }
    [Inject]
    public IGameMusicManagerModel gameMusicManagerModel { get; set; }

    public override void OnRegister()
    {
        view.dispatcher.AddListener(GameOverPanelEvent.TryAgain, OnRestartGame);
        FillHighestScore();
        InitGameOverMusic();
    }

    private void FillHighestScore()
    {
        view.highScoreText.text = $"HIGHEST SCORE = {playerAndWeaponUIModel.score}";
    }

    private void InitGameOverMusic()
    {
        gameMusicManagerModel.audioSource.clip = gameMusicManagerModel.gameOverClip;
        gameMusicManagerModel.audioSource.loop = false;
        gameMusicManagerModel.audioSource.PlayDelayed(3.25f);
    }
    public void OnRestartGame()
    {
        dispatcher.Dispatch(GameOverPanelEvent.RestartGame);
    }

    public override void OnRemove()
    {
        view.dispatcher.RemoveListener(GameOverPanelEvent.TryAgain, OnRestartGame);
    }
}
