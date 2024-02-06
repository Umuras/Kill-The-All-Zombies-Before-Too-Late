using strange.examples.myfirstproject;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.injector.impl;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MVCSContext
{
    public GameContext(MonoBehaviour view) : base(view)
    {
    }

    public GameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {

        injectionBinder.Bind<IBundleModel>().To<BundleModel>();
        injectionBinder.Bind<IUIPanelModel>().To<UIPanelModel>().ToSingleton();
        injectionBinder.Bind<IPlayerMoveModel>().To<PlayerMoveModel>().ToSingleton();
        injectionBinder.Bind<IPlayerCameraModel>().To<PlayerCameraModel>().ToSingleton();
        injectionBinder.Bind<ILootBoxModel>().To<LootBoxModel>().ToSingleton();
        injectionBinder.Bind<IInputModel>().To<InputModel>().ToSingleton();
        injectionBinder.Bind<IWeaponModel>().To<WeaponModel>().ToSingleton();
        injectionBinder.Bind<IObjectPoolingModel>().To<ObjectPoolingModel>().ToSingleton();
        injectionBinder.Bind<IPlayerAndWeaponUIModel>().To<PlayerAndWeaponUIModel>().ToSingleton();
        injectionBinder.Bind<ITargetModel>().To<TargetModel>();
        
        mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
        mediationBinder.Bind<UIPanelControllerView>().To<UIPanelControllerMediator>();
        mediationBinder.Bind<PlayerMoveView>().To<PlayerMoveMediator>();
        mediationBinder.Bind<PlayerCameraView>().To<PlayerCameraMediator>();
        mediationBinder.Bind<MoveCameraView>().To<MoveCameraMediator>();
        mediationBinder.Bind<LootBoxView>().To<LootBoxMediator>();
        mediationBinder.Bind<WeaponView>().To<WeaponMediator>();
        mediationBinder.Bind<ObjectPoolingView>().To<ObjectPoolingMediator>();
        mediationBinder.Bind<BulletView>().To<BulletMediator>();
        mediationBinder.Bind<PlayerAndWeaponUIView>().To<PlayerAndWeaponUIMediator>();
        mediationBinder.Bind<TargetView>().To<TargetMediator>();


        commandBinder.Bind(ContextEvent.START).To<GameInitializeCommand>().Once();
        commandBinder.Bind(FireWeaponEvent.FIREWEAPON).To<FireWeaponCommand>();
    }
}
