using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Zenject;
using System.Linq;
using Assets.Scripts.Helpers;

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
        Container.Bind<MainState>().AsSingle();
        Container.Bind<LoadState>().AsSingle();
        Container.Bind<SettingsState>().AsSingle();
        Container.Bind<StartState>().AsSingle();
    }

    private void InstallViews()
    {
        Container.Bind<Layout>().FromInstance(_uiManager.Layout).AsSingle();
        Container.Bind<MainScreen>().FromInstance(_uiManager.MainScreen).AsSingle();
        Container.Bind<LoadScreen>().FromInstance(_uiManager.LoadScreen).AsSingle();
        Container.Bind<SettingsScreen>().FromInstance(_uiManager.SettingsScreen).AsSingle();
        Container.Bind<SelectionScreen>().FromInstance(_uiManager.SelectionScreen).AsSingle();
        Container.Bind<StartScreen>().FromInstance(_uiManager.StartScreen).AsSingle();
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

        Container.DeclareSignal<MainSceneOpenSignal>();
        Container.DeclareSignal<LoadSceneOpenSignal>();
        Container.DeclareSignal<SettingsSceneOpenSignal>();
        Container.DeclareSignal<SelectionSceneOpenSignal>();
        Container.DeclareSignal<StartSceneOpenSignal>();
        Container.DeclareSignal<BackButtonPressedSignal>();
    }

    void InstallExecutionOrder()
    {
        Container.BindExecutionOrder<AppController>(-10);
    }
}


