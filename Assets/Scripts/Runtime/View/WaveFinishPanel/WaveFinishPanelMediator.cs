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
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(WaveFinishPanelEvent.WaveFinishCountdownStart, OnWaveFinishCountdownStart);
    }

    public void OnWaveFinishCountdownStart()
    {
        StartCoroutine("WaveCountdown");
    }

    IEnumerator WaveCountdown()
    {
        float keepCurrentGameTime = playerAndWeaponUIModel.gameTime;
        playerAndWeaponUIModel.playerMissionLabel.gameObject.SetActive(false);
        playerAndWeaponUIModel.gameTimeLabel.gameObject.SetActive(false);
        playerAndWeaponUIModel.weaponCrossHair.gameObject.SetActive(false);
        playerAndWeaponUIModel.scoreText.gameObject.SetActive(false);
        playerAndWeaponUIModel.waveNumber.gameObject.SetActive(false);

        while (view.waveDuration >= 0)
        {
            view.waveCounterText.text = "" + Math.Round(view.waveDuration, 0);
            yield return new WaitForSeconds(1f);
            view.waveDuration -= 1;
        }

        playerAndWeaponUIModel.gameTime = keepCurrentGameTime;
        playerAndWeaponUIModel.playerMissionLabel.gameObject.SetActive(true);
        playerAndWeaponUIModel.gameTimeLabel.gameObject.SetActive(true);
        playerAndWeaponUIModel.weaponCrossHair.gameObject.SetActive(true);
        playerAndWeaponUIModel.scoreText.gameObject.SetActive(true);
        playerAndWeaponUIModel.waveNumber.gameObject.SetActive(true);

        dispatcher.Dispatch(EnemySpawnerEvent.StartWaveAndSetEnemyVisible);
        enemySpawnerModel.checkingFinishedForDeadEnemies = false;
        uIPanelModel.ClosePanel(2);
    }


    public override void OnRemove()
    {
        dispatcher.RemoveListener(WaveFinishPanelEvent.WaveFinishCountdownStart, OnWaveFinishCountdownStart);
    }
}
