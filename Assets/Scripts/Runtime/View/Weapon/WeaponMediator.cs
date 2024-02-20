using strange.extensions.mediation.impl;
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
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }

    public override void OnRegister()
    {
        Cursor.lockState = CursorLockMode.Locked;
        weaponModel.InitializeWeaponsProperties(fireAudioSource: view.fireAudioSource, reloadAudioSource: view.reloadAudioSource, 
            fireAnimation: view.fireAnimation, firstWeapon: WeaponKeys.Pistol, weaponMuzzleTransform: view.weaponMuzzleTransform, 
            pistolParticle: view.pistolParticleSystem, rifleParticle: view.rifleParticleSystem);
    }

    private void Update()
    {
        if (uIPanelModel.isOpenPanel)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!weaponModel.reloading)
            {
                weaponModel.ChangeWeapon(view.weaponList, WeaponKeys.Pistol, view.fireAnimation, view.reloadAudioSource);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!weaponModel.reloading)
            {
                weaponModel.ChangeWeapon(view.weaponList, WeaponKeys.Rifle, view.fireAnimation, view.reloadAudioSource);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponModel.Reload(view.reloadAudioSource);
        }

        if (!weaponModel.reloading)
        {
            dispatcher.Dispatch(FireWeaponEvent.FIREWEAPON);
        }
    }
}
