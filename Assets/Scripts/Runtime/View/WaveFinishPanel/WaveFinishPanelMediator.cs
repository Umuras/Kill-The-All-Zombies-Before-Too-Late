using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveFinishPanelEvent
{
    WaveFinishCountdownStart
}

public class WaveFinishPanelMediator : EventMediator
{
    [Inject]
    public WaveFinishPanelView view { get; set; }
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }
    [Inject]
    public IEnemySpawnerModel enemySpawnerModel { get; set; }

    public override void OnRegister()
    {
        view.waveDuration = 10;
        dispatcher.AddListener(WaveFinishPanelEvent.WaveFinishCountdownStart, OnWaveFinishCountdownStart);
    }

    public void OnWaveFinishCountdownStart()
    {
        StartCoroutine("WaveCountdown");
    }

    IEnumerator WaveCountdown()
    {
        while (view.waveDuration >= 0)
        {
            view.waveCounterText.text = "" + Math.Round(view.waveDuration, 0);
            yield return new WaitForSeconds(1f);
            view.waveDuration -= 1;
        }

        dispatcher.Dispatch(EnemySpawnerEvent.StartWaveAndSetEnemyVisible);
        enemySpawnerModel.checkingFinishedForDeadEnemies = false;
        uIPanelModel.ClosePanel(2);
    }


    public override void OnRemove()
    {
        dispatcher.RemoveListener(WaveFinishPanelEvent.WaveFinishCountdownStart, OnWaveFinishCountdownStart);
    }
}
