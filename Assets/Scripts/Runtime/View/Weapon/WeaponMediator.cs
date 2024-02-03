using strange.extensions.mediation.impl;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public enum WeaponEvent
{
    DecreasingAmmo,
    AmmoFinished
}

public class WeaponMediator : EventMediator
{
    [Inject]
    public WeaponView view { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public override void OnRegister()
    {
        weaponModel.InitializeWeaponsProperties();
        Cursor.lockState = CursorLockMode.Locked;
        weaponModel.weaponIndex = 0;
        view.fireAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.fire;
        view.reloadAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.realod;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            view.weaponList[weaponModel.weaponIndex].SetActive(false);
            weaponModel.weaponIndex = 0;
            view.weaponList[weaponModel.weaponIndex].SetActive(true);
            playerAndWeaponUIModel.ChangeWeaponAmmoText(weaponModel.totalPistolMagInside, weaponModel.totalPistolAmmo);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            view.weaponList[weaponModel.weaponIndex].SetActive(false);
            weaponModel.weaponIndex = 1;
            view.weaponList[weaponModel.weaponIndex].SetActive(true);
            playerAndWeaponUIModel.ChangeWeaponAmmoText(weaponModel.totalRifleMagInside, weaponModel.totalRifleAmmo);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponModel.Reload(view.reloadAudioSource);
            playerAndWeaponUIModel.statusLabel.text = " ";
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            if (weaponModel.totalPistolMagInside > 0)
            {
                if (!view.fireAnimation.isPlaying)
                {
                    playerAndWeaponUIModel.DecreasingAmmo(weaponModel.weaponIndex, --weaponModel.totalPistolMagInside);
                    weaponModel.RaycastForWeapon(view.weaponMuzzleTransform[weaponModel.weaponIndex], weaponModel.pistolShootRange);
                    view.fireAnimation.Play();
                    view.fireAudioSource.Play();
                } 
            }
            else
            {
                playerAndWeaponUIModel.AmmoFinished("Your ammo is finished, PLEASE RELOAD!!!");
            }
        }
    }
}
