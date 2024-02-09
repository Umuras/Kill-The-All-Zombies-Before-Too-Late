using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAndWeaponUIModel : IPlayerAndWeaponUIModel
{
    [Inject]
    public IWeaponModel weaponModel { get; set; }

    public TextMeshProUGUI healthText { get; set; }
    public TextMeshProUGUI ammoText { get; set; }
    public TextMeshProUGUI statusLabel { get; set; }

    public TextMeshProUGUI playerMissionLabel { get; set; }

    public Image weaponCrossHair { get; set; }

    public void FillTexts(TextMeshProUGUI healthText, TextMeshProUGUI ammoText, TextMeshProUGUI statusLabel, TextMeshProUGUI playerMissionLabel)
    {
        this.healthText = healthText;
        this.ammoText = ammoText;
        this.statusLabel = statusLabel;
        this.playerMissionLabel = playerMissionLabel;
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
}
