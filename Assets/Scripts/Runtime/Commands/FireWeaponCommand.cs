using strange.extensions.command.impl;
using strange.extensions.mediation.impl;
using UnityEngine;

public class FireWeaponCommand : EventCommand
{
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public override void Execute()
    {
        if (weaponModel.weaponIndex == (int)WeaponKeys.Pistol)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weaponModel.totalPistolMagInside > 0)
                {
                    if (!weaponModel.fireAnimation.isPlaying)
                    {
                        playerAndWeaponUIModel.DecreasingAmmo(weaponModel.weaponIndex, --weaponModel.totalPistolMagInside);
                        weaponModel.RaycastForWeapon(weaponModel.weaponMuzzleTransform[weaponModel.weaponIndex], weaponModel.pistolShootRange);
                        weaponModel.pistolParticleSystem.Play();
                        weaponModel.fireAnimation.Play(WeaponKeys.Pistol.ToString());
                        weaponModel.fireAudioSource.Play();
                    }
                }
            }
        }
        else if (weaponModel.weaponIndex == (int)WeaponKeys.Rifle)
        {
            if (Input.GetMouseButton(0))
            {
                if (weaponModel.totalRifleMagInside > 0)
                {
                    if (!weaponModel.fireAnimation.isPlaying)
                    {
                        playerAndWeaponUIModel.DecreasingAmmo(weaponModel.weaponIndex, --weaponModel.totalRifleMagInside);
                        weaponModel.RaycastForWeapon(weaponModel.weaponMuzzleTransform[weaponModel.weaponIndex], weaponModel.rifleShootRange);
                        weaponModel.rifleParticleSystem.Play();
                        weaponModel.fireAnimation.Play(WeaponKeys.Rifle.ToString());
                        weaponModel.fireAudioSource.Play();
                    }
                }
            }
        }
    }
}
