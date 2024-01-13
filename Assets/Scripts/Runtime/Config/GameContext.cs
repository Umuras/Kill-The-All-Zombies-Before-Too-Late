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
        
        mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
        mediationBinder.Bind<UIPanelControllerView>().To<UIPanelControllerMediator>();


        commandBinder.Bind(ContextEvent.START).To<GameInitializeCommand>().Once();
    }
}
