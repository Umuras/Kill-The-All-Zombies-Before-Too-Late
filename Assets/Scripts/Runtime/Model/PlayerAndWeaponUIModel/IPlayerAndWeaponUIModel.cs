using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IPlayerAndWeaponUIModel
{
    TextMeshProUGUI healthText { get; set; }
    TextMeshProUGUI ammoText { get; set; }
    TextMeshProUGUI statusLabel { get; set; }

    void InitTextAmmo(float weaponMagCapacity, float totalWeaponAmmo);

    void FillTexts(TextMeshProUGUI healthText, TextMeshProUGUI ammoText, TextMeshProUGUI statusLabel);

    void DecreasingAmmo(int weaponIndex, float totalWeaponMagInside);

    void AmmoFinished(string RealodWarningText);

    void ReloadAmmo(float totalWeaponMagInside, float totalWeaponAmmo,float weaponMagInside);

    void ChangeWeaponAmmoText(float weaponMagInside, float totalWeaponAmmo);
}
