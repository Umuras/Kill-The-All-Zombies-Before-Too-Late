using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAndWeaponUIModel : IPlayerAndWeaponUIModel
{
    [Inject]
    public IWeaponModel weaponModel { get; set; }

    public TextMeshProUGUI healthText { get; set; }
    public TextMeshProUGUI ammoText { get; set; }
    public TextMeshProUGUI statusLabel { get; set; }

    public void FillTexts(TextMeshProUGUI healthText, TextMeshProUGUI ammoText, TextMeshProUGUI statusLabel)
    {
        this.healthText = healthText;
        this.ammoText = ammoText;
        this.statusLabel = statusLabel;
    }

    public void InitTextAmmo(float weaponMagCapacity, float totalWeaponAmmo)
    {
        ammoText.text = weaponMagCapacity.ToString() + "/" + totalWeaponAmmo;
    }

    public void DecreasingAmmo(int weaponIndex, float totalWeaponMagInside)
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

    public void ReloadAmmo(float totalWeaponMagInside, float totalWeaponAmmo, float weaponMagInside)
    {
        float requiredAmmo = totalWeaponMagInside - weaponMagInside;
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
    }

    public void ChangeWeaponAmmoText(float weaponMagInside, float totalWeaponAmmo)
    {
        ammoText.text = weaponMagInside + "/" + totalWeaponAmmo;
    }
}
