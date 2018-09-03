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
        Container.Bind<Screen1State>().AsSingle();
        Container.Bind<Screen2State>().AsSingle();
    }

    private void InstallViews()
    {
        Container.Bind<MenuScreen>().FromInstance(_uiManager.MenuScreen).AsSingle();
        Container.Bind<MainScreen>().FromInstance(_uiManager.MainScreen).AsSingle();
        Container.Bind<LoadScreen>().FromInstance(_uiManager.LoadScreen).AsSingle();
        Container.Bind<Screen1Screen>().FromInstance(_uiManager.Screen1Screen).AsSingle();
        Container.Bind<Screen2Screen>().FromInstance(_uiManager.Screen2Screen).AsSingle();
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
        Container.DeclareSignal<Screen1SceneOpenSignal>();
        Container.DeclareSignal<Screen2SceneOpenSignal>();
    }

    void InstallExecutionOrder()
    {
        Container.BindExecutionOrder<AppController>(-10);
    }
}


