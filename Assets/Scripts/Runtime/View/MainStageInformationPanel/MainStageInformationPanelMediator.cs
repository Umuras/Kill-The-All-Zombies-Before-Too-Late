using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainStageInformationPanelEvent
{
    EnterMainStage
}

public class MainStageInformationPanelMediator : EventMediator
{
    [Inject]
    public MainStageInformationPanelView view { get; set; }
    [Inject]
    public IBundleModel bundleModel { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IGameAreaModel gameAreaModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }
    [Inject]
    public IPlayerModel playerModel { get; set; }
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }
    [Inject]
    public IEnemySpawnerModel enemySpawnerModel { get; set; }

    public override void OnRegister()
    {
        view.dispatcher.AddListener(MainStageInformationPanelEvent.EnterMainStage, OnEnterMainStage);
    }

    private void OnEnterMainStage()
    {
        view.readyButton.interactable = false;
        bundleModel.AddressableInstantiate(GameAreaKeys.MAINSTAGE, gameAreaModel.GameAreaTransform).Then(() =>
        {
            weaponModel.ResetWeaponAmmo();
            CharacterController playerCharacterController = playerModel.player.GetComponent<CharacterController>();
            playerCharacterController.enabled = false;
            //MainStage Center Position
            playerCharacterController.gameObject.transform.position = new Vector3(53.05114f, 0, 39.10672f);
            playerCharacterController.enabled = true;
        }).Then(() =>
        {
            playerAndWeaponUIModel.gameTimeLabel.gameObject.SetActive(true);
            playerAndWeaponUIModel.scoreText.gameObject.SetActive(true);
            gameAreaModel.isOpenedMainStage = true;
            uIPanelModel.isOpenPanel = false;
            uIPanelModel.ClosePanel(2);
            dispatcher.Dispatch(EnemySpawnerEvent.StartWaveAndSetEnemyVisible);
        });
        
    }

    public override void OnRemove()
    {
        view.dispatcher.RemoveListener(MainStageInformationPanelEvent.EnterMainStage, OnEnterMainStage);
    }
}
