using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMediator : EventMediator
{
    [Inject]
    public AmmoView view { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(view.PlayerTag))
        {
            if (AmmoQuantityControl())
            {
                if (weaponModel.weaponIndex == (int)WeaponKeys.Pistol)
                {
                    weaponModel.totalPistolMagInside = weaponModel.pistolMagCapacity;
                    weaponModel.totalPistolAmmo = weaponModel.pistolMagCount * weaponModel.pistolMagCapacity;
                    playerAndWeaponUIModel.InitTextAmmo(weaponModel.totalPistolMagInside, weaponModel.totalPistolAmmo);
                }
                else if (weaponModel.weaponIndex == (int)WeaponKeys.Rifle)
                {
                    weaponModel.totalRifleMagInside = weaponModel.rifleMagCapacity;
                    weaponModel.totalRifleAmmo = weaponModel.rifleMagCount * weaponModel.rifleMagCapacity;
                    playerAndWeaponUIModel.InitTextAmmo(weaponModel.totalRifleMagInside, weaponModel.totalRifleAmmo);
                }
                gameObject.SetActive(false);
            }
        }
    }

    private bool AmmoQuantityControl()
    {
        if (weaponModel.weaponIndex == (int)WeaponKeys.Pistol)
        {
            if (weaponModel.totalPistolMagInside == weaponModel.pistolMagCapacity && weaponModel.totalPistolAmmo == weaponModel.pistolMagCount * weaponModel.pistolMagCapacity)
            {
                return false;
            }
        }
        else if (weaponModel.weaponIndex == (int)WeaponKeys.Rifle)
        {
            if (weaponModel.totalRifleMagInside == weaponModel.rifleMagCapacity  && weaponModel.totalRifleAmmo == weaponModel.rifleMagCount * weaponModel.rifleMagCapacity)
            {
                return false;
            }
        }

        return true;
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
