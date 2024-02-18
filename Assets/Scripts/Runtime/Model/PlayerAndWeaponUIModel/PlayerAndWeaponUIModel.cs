using DG.Tweening;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAndWeaponUIModel : IPlayerAndWeaponUIModel
{
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }

    public TextMeshProUGUI healthText { get; set; }
    public TextMeshProUGUI ammoText { get; set; }
    public TextMeshProUGUI statusLabel { get; set; }

    public TextMeshProUGUI playerMissionLabel { get; set; }

    public TextMeshProUGUI gameTimeLabel { get; set; }
    public TextMeshProUGUI multiplyTwoDamageLabel { get; set; }
    public TextMeshProUGUI multiplyTwoSpeedLabel { get; set; }
    public TextMeshProUGUI waveNumber { get; set; }
    public TextMeshProUGUI scoreText { get; set; }

    public Image weaponCrossHair { get; set; }

    public float gameTime { get; set; }

    public float increaseDamagePowerFinishTime { get; set; }
    public float increasePlayerMoveSpeedFinishTime { get; set; }
    public int score { get; set; }
    private bool _gameTimeFinished;

    public void FillTexts(TextMeshProUGUI healthText, TextMeshProUGUI ammoText, TextMeshProUGUI statusLabel,
        TextMeshProUGUI playerMissionLabel, TextMeshProUGUI gameTimeLabel, TextMeshProUGUI multiplyTwoDamageLabel,
        TextMeshProUGUI multiplyTwoSpeedLabel, TextMeshProUGUI waveNumber, TextMeshProUGUI scoreText)
    {
        this.healthText = healthText;
        this.ammoText = ammoText;
        this.statusLabel = statusLabel;
        this.playerMissionLabel = playerMissionLabel;
        this.gameTimeLabel = gameTimeLabel;
        this.multiplyTwoDamageLabel = multiplyTwoDamageLabel;
        this.multiplyTwoSpeedLabel = multiplyTwoSpeedLabel;
        this.waveNumber = waveNumber;
        this.scoreText = scoreText;
    }

    public void FillCrossHairImage(Image weaponCrossHair)
    {
        this.weaponCrossHair = weaponCrossHair;
    }

    public void InitTextAmmo(int weaponMagCapacity, int totalWeaponAmmo)
    {
        ammoText.text = weaponMagCapacity.ToString() + "/" + totalWeaponAmmo;
    }

    public void DecreasingAmmo(int weaponIndex, int totalWeaponMagInside)
    {
        if (weaponIndex == (int)WeaponKeys.Pistol)
        {
            ammoText.text = totalWeaponMagInside.ToString() + "/" + weaponModel.totalPistolAmmo;
        }
        else
        {
            ammoText.text = totalWeaponMagInside.ToString() + "/" + weaponModel.totalRifleAmmo;
        }
    }
    public void AmmoFinished(string RealodWarningText)
    {
        statusLabel.text = RealodWarningText;
    }

    public void ReloadAmmo(int totalWeaponMagInside, int totalWeaponAmmo, int weaponMagInside)
    {
        int requiredAmmo = totalWeaponMagInside - weaponMagInside;
        if (totalWeaponAmmo > requiredAmmo)
        {
            totalWeaponAmmo = (totalWeaponAmmo - requiredAmmo);
            weaponMagInside = weaponMagInside + requiredAmmo;
        }
        else if (totalWeaponAmmo <= requiredAmmo)
        {
            if (totalWeaponAmmo + weaponMagInside > totalWeaponMagInside)
            {
                totalWeaponAmmo = totalWeaponAmmo - requiredAmmo;
                weaponMagInside = weaponMagInside + requiredAmmo;
            }
            else
            {
                weaponMagInside = weaponMagInside + totalWeaponAmmo;
                totalWeaponAmmo = 0;
            }
        }

        ammoText.text = weaponMagInside + "/" + totalWeaponAmmo;

        if (weaponModel.weaponIndex == (int)WeaponKeys.Pistol)
        {
            weaponModel.totalPistolMagInside = weaponMagInside;
            weaponModel.totalPistolAmmo = totalWeaponAmmo;
        }
        else
        {
            weaponModel.totalRifleMagInside = weaponMagInside;
            weaponModel.totalRifleAmmo = totalWeaponAmmo;
        }

        weaponModel.reloading = false;
        statusLabel.text = " ";
        weaponCrossHair.gameObject.SetActive(true);
    }

    public void ChangeWeaponAmmoText(int weaponMagInside, int totalWeaponAmmo)
    {
        ammoText.text = weaponMagInside + "/" + totalWeaponAmmo;
    }

    public void UpdatePlayerHealthText(int playerHealth)
    {
        healthText.text = "+= " + playerHealth;
    }

    public void MainStageGameTimer()
    {
        if (_gameTimeFinished)
        {
            return;
        }

        if (gameTime > 0)
        {
            gameTime = (gameTime - Time.deltaTime);
            gameTimeLabel.text = "Time = \r\n" + (Math.Round(gameTime,0));
        }
        else
        {
            _gameTimeFinished = true;
            gameTimeLabel.text = "Time = \r\n" + 0;
            PlayerDeath();
        }
    }

    public void IncreaseDamagePowerTimer()
    {
        if (increaseDamagePowerFinishTime > 0)
        {
            increaseDamagePowerFinishTime -= Time.deltaTime;
            multiplyTwoDamageLabel.text = $"2x Damage Time = {Math.Round(increaseDamagePowerFinishTime,0)}";
        }
        else
        {
            multiplyTwoDamageLabel.gameObject.SetActive(false);
            weaponModel.ResetWeaponDamagePower();
        }
    }

    public void IncreasePlayerMoveSpeedTimer()
    {
        if (increasePlayerMoveSpeedFinishTime > 0)
        {
            increasePlayerMoveSpeedFinishTime -= Time.deltaTime;
            multiplyTwoSpeedLabel.text = $"2x Speed Time = {Math.Round(increasePlayerMoveSpeedFinishTime, 0)}";
        }
        else
        {
            multiplyTwoSpeedLabel.gameObject.SetActive(false);
            playerMoveModel.ResetPlayerMoveSpeed();
        }
    }

    public void PlayerDeath()
    {
        uIPanelModel.isOpenPanel = true;
        uIPanelModel.OpenPanel(2, PanelKeys.GAMEOVERPANEL);
    }
}
