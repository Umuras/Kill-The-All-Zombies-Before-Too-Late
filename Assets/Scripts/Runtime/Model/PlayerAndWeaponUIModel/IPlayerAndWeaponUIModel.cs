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
    public Image weaponCrossHair { get; set; }

    void InitTextAmmo(int weaponMagCapacity, int totalWeaponAmmo);

    void FillTexts(TextMeshProUGUI healthText, TextMeshProUGUI ammoText, TextMeshProUGUI statusLabel);

    void DecreasingAmmo(int weaponIndex, int totalWeaponMagInside);

    void AmmoFinished(string RealodWarningText);

    void ReloadAmmo(int totalWeaponMagInside, int totalWeaponAmmo,int weaponMagInside);

    void ChangeWeaponAmmoText(int weaponMagInside, int totalWeaponAmmo);

    void FillCrossHairImage(Image weaponCrossHair);
}
