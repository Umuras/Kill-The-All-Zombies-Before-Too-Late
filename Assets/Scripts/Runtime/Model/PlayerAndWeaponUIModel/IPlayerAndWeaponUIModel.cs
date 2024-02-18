using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerAndWeaponUIModel
{
    TextMeshProUGUI healthText { get; set; }
    TextMeshProUGUI ammoText { get; set; }
    TextMeshProUGUI statusLabel { get; set; }
    TextMeshProUGUI playerMissionLabel { get; set; }
    TextMeshProUGUI gameTimeLabel { get; set; }
    TextMeshProUGUI multiplyTwoDamageLabel { get; set; }
    TextMeshProUGUI multiplyTwoSpeedLabel { get; set; }
    TextMeshProUGUI gameOverLabel { get; set; }
    TextMeshProUGUI waveNumber { get; set; }

    Image weaponCrossHair { get; set; }
    Image playerDeathScreen { get; set; }
    float gameTime { get; set; }
    float increaseDamagePowerFinishTime { get; set; }
    float increasePlayerMoveSpeedFinishTime { get; set; }

    void InitTextAmmo(int weaponMagCapacity, int totalWeaponAmmo);

    void FillTexts(TextMeshProUGUI healthText, TextMeshProUGUI ammoText, TextMeshProUGUI statusLabel,
        TextMeshProUGUI playerMissionLabel, TextMeshProUGUI gameTimeLabel, TextMeshProUGUI multiplyTwoDamageLabel,
        TextMeshProUGUI multiplyTwoSpeedLabel, TextMeshProUGUI gameOverLabel, TextMeshProUGUI waveNumber);

    void DecreasingAmmo(int weaponIndex, int totalWeaponMagInside);

    void AmmoFinished(string RealodWarningText);

    void ReloadAmmo(int totalWeaponMagInside, int totalWeaponAmmo,int weaponMagInside);

    void ChangeWeaponAmmoText(int weaponMagInside, int totalWeaponAmmo);

    void FillCrossHairImage(Image weaponCrossHair);
    void UpdatePlayerHealthText(int playerHealth);

    void MainStageGameTimer();

    void IncreaseDamagePowerTimer();
    void IncreasePlayerMoveSpeedTimer();
    void PlayerDeath();
}