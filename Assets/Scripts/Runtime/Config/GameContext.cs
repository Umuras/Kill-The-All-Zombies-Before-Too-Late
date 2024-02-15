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
        injectionBinder.Bind<IPlayerAndWeaponUIModel>().To<PlayerAndWeaponUIModel>().ToSingleton();
        injectionBinder.Bind<IWeaponModel>().To<WeaponModel>().ToSingleton();
        injectionBinder.Bind<IObjectPoolingModel>().To<ObjectPoolingModel>().ToSingleton();
        injectionBinder.Bind<ITargetModel>().To<TargetModel>().ToSingleton();
        injectionBinder.Bind<IGameAreaModel>().To<GameAreaModel>().ToSingleton();
        injectionBinder.Bind<IEnemyModel>().To<EnemyModel>();
        injectionBinder.Bind<IPlayerModel>().To<PlayerModel>().ToSingleton();
        injectionBinder.Bind<IEnemySpawnerModel>().To<EnemySpawnerModel>().ToSingleton();
        
        mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
        mediationBinder.Bind<UIPanelControllerView>().To<UIPanelControllerMediator>();
        mediationBinder.Bind<PlayerMoveView>().To<PlayerMoveMediator>();
        mediationBinder.Bind<PlayerCameraView>().To<PlayerCameraMediator>();
        mediationBinder.Bind<MoveCameraView>().To<MoveCameraMediator>();
        mediationBinder.Bind<LootBoxView>().To<LootBoxMediator>();
        mediationBinder.Bind<PlayerAndWeaponUIView>().To<PlayerAndWeaponUIMediator>();
        mediationBinder.Bind<WeaponView>().To<WeaponMediator>();
        mediationBinder.Bind<ObjectPoolingView>().To<ObjectPoolingMediator>();
        mediationBinder.Bind<BulletView>().To<BulletMediator>();
        mediationBinder.Bind<TargetView>().To<TargetMediator>();
        mediationBinder.Bind<AmmoView>().To<AmmoMediator>();
        mediationBinder.Bind<TargetCounterView>().To<TargetCounterMediator>();
        mediationBinder.Bind<TrainingInformationPanelView>().To<TrainingInformationPanelMediator>();
        mediationBinder.Bind<PortalView>().To<PortalMediator>();
        mediationBinder.Bind<GameAreaView>().To<GameAreaMediator>();
        mediationBinder.Bind<EnemyView>().To<EnemyMediator>();
        mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
        mediationBinder.Bind<EnemyHandView>().To<EnemyHandMediator>();
        mediationBinder.Bind<HealthView>().To<HealthMediator>();
        mediationBinder.Bind<MainStageInformationPanelView>().To<MainStageInformationPanelMediator>();
        mediationBinder.Bind<IncreaseDamagePowerView>().To<IncreaseDamagePowerMediator>();
        mediationBinder.Bind<IncreaseGameTimeView>().To<IncreaseGameTimeMediator>();
        mediationBinder.Bind<IncreasePlayerSpeedView>().To<IncreasePlayerSpeedMediator>();
        mediationBinder.Bind<EnemySpawnerView>().To<EnemySpawnerMediator>();


        commandBinder.Bind(ContextEvent.START).To<GameInitializeCommand>().Once();
        commandBinder.Bind(FireWeaponEvent.FIREWEAPON).To<FireWeaponCommand>();
    }
}
