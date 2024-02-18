using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using DG.Tweening;
using UnityEngine;

public class PlayerAndWeaponUIMediator : EventMediator
{
    [Inject]
    public PlayerAndWeaponUIView view { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }
    [Inject]
    public IGameAreaModel gameAreaModel { get; set; }
    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }

    public override void OnRegister()
    {
        playerAndWeaponUIModel.FillTexts(view.healtText, view.ammoText, view.statusLabel, view.playerMissionLabel,
            view.gameTimeLabel, view.multiplyTwoDamageLabel, view.multiplyTwoSpeedLabel, view.waveNumber, view.scoreText);
        playerAndWeaponUIModel.FillCrossHairImage(view.weaponCrossHair);
        playerAndWeaponUIModel.InitTextAmmo(weaponModel.pistolMagCapacity, weaponModel.totalPistolAmmo);
        playerAndWeaponUIModel.gameTime = view.gameStartInitialTime;
        playerAndWeaponUIModel.increaseDamagePowerFinishTime = 20;
        playerAndWeaponUIModel.increasePlayerMoveSpeedFinishTime = 20;
    }

    private void Update()
    {
        if (gameAreaModel.isOpenedMainStage)
        {
            playerAndWeaponUIModel.MainStageGameTimer();
        }

        if (weaponModel.isWeaponIncreaseDamage)
        {
            playerAndWeaponUIModel.IncreaseDamagePowerTimer();
        }

        if (playerMoveModel.isPlayerSpeedIncreased)
        {
            playerAndWeaponUIModel.IncreasePlayerMoveSpeedTimer();
        }
    }
}
