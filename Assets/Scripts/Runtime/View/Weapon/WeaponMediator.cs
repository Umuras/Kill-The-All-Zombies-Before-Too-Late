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
        view.fireAnimation.AddClip(weaponModel.weaponData[0].weapon.fireAnimationClip, WeaponKeys.Pistol.ToString());
        view.fireAnimation.AddClip(weaponModel.weaponData[1].weapon.fireAnimationClip, WeaponKeys.Rifle.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            view.weaponList[weaponModel.weaponIndex].SetActive(false);
            weaponModel.weaponIndex = 0;
            view.weaponList[weaponModel.weaponIndex].SetActive(true);
            playerAndWeaponUIModel.ChangeWeaponAmmoText(weaponModel.totalPistolMagInside, weaponModel.totalPistolAmmo);
            view.fireAnimation.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.fireAnimationClip;
            view.reloadAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.realod;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            view.weaponList[weaponModel.weaponIndex].SetActive(false);
            weaponModel.weaponIndex = 1;
            view.weaponList[weaponModel.weaponIndex].SetActive(true);
            playerAndWeaponUIModel.ChangeWeaponAmmoText(weaponModel.totalRifleMagInside, weaponModel.totalRifleAmmo);
            view.fireAnimation.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.fireAnimationClip;
            view.reloadAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.realod;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponModel.reloading = true;
            weaponModel.Reload(view.reloadAudioSource);
            playerAndWeaponUIModel.statusLabel.text = " ";
        }

        if (!weaponModel.reloading)
        {
            if (weaponModel.weaponIndex == (int)WeaponKeys.Pistol)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (weaponModel.totalPistolMagInside > 0)
                    {
                        if (!view.fireAnimation.isPlaying)
                        {
                            playerAndWeaponUIModel.DecreasingAmmo(weaponModel.weaponIndex, --weaponModel.totalPistolMagInside);
                            weaponModel.RaycastForWeapon(view.weaponMuzzleTransform[weaponModel.weaponIndex], weaponModel.pistolShootRange);
                            view.pistolParticleSystem.Play();
                            view.fireAnimation.Play(WeaponKeys.Pistol.ToString());
                            view.fireAudioSource.Play();
                        }
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    if (weaponModel.totalRifleMagInside > 0)
                    {
                        if (!view.fireAnimation.isPlaying)
                        {
                            playerAndWeaponUIModel.DecreasingAmmo(weaponModel.weaponIndex, --weaponModel.totalRifleMagInside);
                            weaponModel.RaycastForWeapon(view.weaponMuzzleTransform[weaponModel.weaponIndex], weaponModel.rifleShootRange);
                            view.rifleParticleSystem.Play();
                            view.fireAnimation.Play(WeaponKeys.Rifle.ToString());
                            view.fireAudioSource.Play();
                        }
                    }
                }
            }
        }
    }
}
