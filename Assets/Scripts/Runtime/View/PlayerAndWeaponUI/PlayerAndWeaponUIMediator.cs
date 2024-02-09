using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using DG.Tweening;

public class PlayerAndWeaponUIMediator : EventMediator
{
    [Inject]
    public PlayerAndWeaponUIView view { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public override void OnRegister()
    {
        playerAndWeaponUIModel.FillTexts(view.healtText, view.ammoText, view.statusLabel, view.playerMissionLabel);
        playerAndWeaponUIModel.FillCrossHairImage(view.weaponCrossHair);
        playerAndWeaponUIModel.InitTextAmmo(weaponModel.pistolMagCapacity, weaponModel.totalPistolAmmo);
    }

}
