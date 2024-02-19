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
    [Inject]
    public IGameMusicManagerModel gameMusicManagerModel { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }

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
        float currentMainStageMusicTime = gameMusicManagerModel.audioSource.time;
        PutWaveMusic();
        float keepCurrentGameTime = playerAndWeaponUIModel.gameTime;
        bool inActiveDamagePowerUp = false;
        bool inActiveSpeedPowerUp = false;
        if (playerAndWeaponUIModel.multiplyTwoDamageLabel.gameObject.activeInHierarchy)
        {
            playerAndWeaponUIModel.multiplyTwoDamageLabel.gameObject.SetActive(false);
            weaponModel.isWeaponIncreaseDamage = false;
            inActiveDamagePowerUp = true;
        }
        if (playerAndWeaponUIModel.multiplyTwoSpeedLabel.gameObject.activeInHierarchy)
        {
            playerAndWeaponUIModel.multiplyTwoSpeedLabel.gameObject.SetActive(false);
            playerMoveModel.isPlayerSpeedIncreased = false;
            inActiveSpeedPowerUp = true;
        }
        playerAndWeaponUIModel.playerMissionLabel.gameObject.SetActive(false);
        playerAndWeaponUIModel.gameTimeLabel.gameObject.SetActive(false);
        playerAndWeaponUIModel.weaponCrossHair.gameObject.SetActive(false);
        playerAndWeaponUIModel.scoreText.gameObject.SetActive(false);
        playerAndWeaponUIModel.waveNumber.gameObject.SetActive(false);
        
        playerAndWeaponUIModel.multiplyTwoSpeedLabel.gameObject.SetActive(false);

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

        if (inActiveSpeedPowerUp)
        {
            playerAndWeaponUIModel.multiplyTwoSpeedLabel.gameObject.SetActive(true);
            playerMoveModel.isPlayerSpeedIncreased = false;
        }
        if (inActiveDamagePowerUp)
        {
            playerAndWeaponUIModel.multiplyTwoDamageLabel.gameObject.SetActive(true);
            weaponModel.isWeaponIncreaseDamage = true;
        }

        dispatcher.Dispatch(EnemySpawnerEvent.StartWaveAndSetEnemyVisible);
        enemySpawnerModel.checkingFinishedForDeadEnemies = false;
        PutMainStageMusic(currentMainStageMusicTime);
        uIPanelModel.ClosePanel(2);
    }

    private void PutWaveMusic()
    {
        gameMusicManagerModel.audioSource.clip = gameMusicManagerModel.waveClip;
        gameMusicManagerModel.audioSource.Play();
    }

    private void PutMainStageMusic(float currentMainStageClipTime)
    {
        gameMusicManagerModel.audioSource.clip = gameMusicManagerModel.mainStageClip;
        gameMusicManagerModel.audioSource.time = currentMainStageClipTime;
        gameMusicManagerModel.audioSource.Play();
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(WaveFinishPanelEvent.WaveFinishCountdownStart, OnWaveFinishCountdownStart);
    }
}
