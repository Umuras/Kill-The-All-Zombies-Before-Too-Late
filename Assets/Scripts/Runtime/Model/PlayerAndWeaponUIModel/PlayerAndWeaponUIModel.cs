using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAndWeaponUIModel : IPlayerAndWeaponUIModel
{
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }

    public TextMeshProUGUI healthText { get; set; }
    public TextMeshProUGUI ammoText { get; set; }
    public TextMeshProUGUI statusLabel { get; set; }

    public TextMeshProUGUI playerMissionLabel { get; set; }

    public TextMeshProUGUI gameTimeLabel { get; set; }
    public TextMeshProUGUI multiplyTwoDamageLabel { get; set; }
    public TextMeshProUGUI multiplyTwoSpeedLabel { get; set; }

    public Image weaponCrossHair { get; set; }

    public float gameTime { get; set; }

    private float _increaseDamagePowerFinishTime = 20;
    private float _increasePlayerMoveSpeedFinishTime = 20;

    public void FillTexts(TextMeshProUGUI healthText, TextMeshProUGUI ammoText, TextMeshProUGUI statusLabel, 
        TextMeshProUGUI playerMissionLabel, TextMeshProUGUI gameTimeLabel, TextMeshProUGUI multiplyTwoDamageLabel,
        TextMeshProUGUI multiplyTwoSpeedLabel)
    {
        this.healthText = healthText;
        this.ammoText = ammoText;
        this.statusLabel = statusLabel;
        this.playerMissionLabel = playerMissionLabel;
        this.gameTimeLabel = gameTimeLabel;
        this.multiplyTwoDamageLabel = multiplyTwoDamageLabel;
        this.multiplyTwoSpeedLabel = multiplyTwoSpeedLabel;
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
       if (!gameTimeLabel.gameObject.activeInHierarchy)
       {
           gameTimeLabel.gameObject.SetActive(true);
       }

       if (gameTime > 0)
       {
           gameTime = (gameTime - Time.deltaTime);
           gameTimeLabel.text = "Time = " + (Math.Round(gameTime,0));
       }
       else
       {
           gameTimeLabel.text = "Time = " + 0;
           Debug.Log("GameOver");
       }
    }

    public void IncreaseDamagePowerTimer()
    {
        if (!multiplyTwoDamageLabel.gameObject.activeInHierarchy)
        {
            multiplyTwoDamageLabel.gameObject.SetActive(true);
        }

        if (_increaseDamagePowerFinishTime > 0)
        {
            _increaseDamagePowerFinishTime -= Time.deltaTime;
            multiplyTwoDamageLabel.text = $"2x Damage Time = {Math.Round(_increaseDamagePowerFinishTime,0)}";
        }
        else
        {
            multiplyTwoDamageLabel.gameObject.SetActive(false);
            weaponModel.ResetWeaponDamagePower();
        }
    }

    public void IncreasePlayerMoveSpeedTimer()
    {
        if (!multiplyTwoSpeedLabel.gameObject.activeInHierarchy)
        {
            multiplyTwoSpeedLabel.gameObject.SetActive(true);
        }

        if (_increasePlayerMoveSpeedFinishTime > 0)
        {
            _increasePlayerMoveSpeedFinishTime -= Time.deltaTime;
            multiplyTwoSpeedLabel.text = $"2x Speed Time = {Math.Round(_increasePlayerMoveSpeedFinishTime, 0)}";
        }
        else
        {
            multiplyTwoSpeedLabel.gameObject.SetActive(false);
            playerMoveModel.ResetPlayerMoveSpeed();
        }
    }
}
