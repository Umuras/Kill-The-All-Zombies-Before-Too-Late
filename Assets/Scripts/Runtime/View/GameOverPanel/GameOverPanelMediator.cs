using strange.extensions.mediation.impl;
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

    public override void OnRegister()
    {
        view.dispatcher.AddListener(GameOverPanelEvent.TryAgain, OnRestartGame);
        FillHighestScore();
    }

    private void FillHighestScore()
    {
        view.highScoreText.text = $"HIGHEST SCORE = {playerAndWeaponUIModel.score}";
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
