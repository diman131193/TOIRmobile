using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Zenject;
using System.Linq;


public class AppInstaller : MonoInstaller
{
    [SerializeField]
    private UIManager _uiManager;

    public override void InstallBindings()
    {
        InstallViews();
        InstallStates();
        InstallSignals();
        InstallMisc();
        InstallExecutionOrder();
    }

    private void InstallStates()
    {
        Container.Bind<MenuState>().AsSingle();
        Container.Bind<MainState>().AsSingle();
        Container.Bind<LoadState>().AsSingle();
    }

    private void InstallViews()
    {
        Container.Bind<MenuScreen>().FromInstance(_uiManager.MenuScreen).AsSingle();
        Container.Bind<MainScreen>().FromInstance(_uiManager.MainScreen).AsSingle();
        Container.Bind<LoadScreen>().FromInstance(_uiManager.LoadScreen).AsSingle();
    }

    void InstallMisc()
    {
        Container.BindInterfacesAndSelfTo<AppController>().AsSingle().NonLazy();
        Container.Bind<StateMachine>().AsSingle().NonLazy();
        Container.Bind<DeviceModel>().AsSingle();
    }

    void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<MenuSceneOpenSignal>();
        Container.DeclareSignal<MainSceneOpenSignal>();
        Container.DeclareSignal<LoadSceneOpenSignal>();
    }

    void InstallExecutionOrder()
    {
        Container.BindExecutionOrder<AppController>(-10);
    }
}


