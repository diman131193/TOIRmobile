using System;
using UnityEngine;
using System.Collections;
using Zenject;
using ModestTree;

    public enum GameStates
    {
        WaitingToStart,
        Playing,
        GameOver,
    }

public class AppController : IInitializable, ITickable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly StateMachine _stateMachine;

    [Inject]
    private MainState mainState;
    [Inject]
    private LoadState loadState;
    [Inject]
    private StartState startState;

    [Inject]
    private SettingsState settingsState;

    [Inject]
    private SelectionState selectionState;

    GameStates _state = GameStates.WaitingToStart;

    public AppController(SignalBus signalBus, StateMachine stateMachine)
    {
        _signalBus = signalBus;
        _stateMachine = stateMachine;
    }

    public GameStates State
    {
        get { return _state; }
    }

    public void Initialize()
    {
        _signalBus.Subscribe<MainSceneOpenSignal>(OnMainSceneOpen);
        _signalBus.Subscribe<LoadSceneOpenSignal>(OnLoadSceneOpen);
        _signalBus.Subscribe<SettingsSceneOpenSignal>(OnSettingsSceneOpen);
        _signalBus.Subscribe<SelectionSceneOpenSignal>(OnSelectionSceneOpen);
        _signalBus.Subscribe<StartSceneOpenSignal>(OnStartSceneOpen);
        _signalBus.Subscribe<BackButtonPressedSignal>(OnBackButtonPressed);
        _signalBus.Fire<StartSceneOpenSignal>();
    }

    private void OnBackButtonPressed()
    {
        _stateMachine.Unload(true);
    }


    private void OnStartSceneOpen()
    {
        _stateMachine.Unload(false);
        _stateMachine.Load(startState);
    }

    private void OnSettingsSceneOpen()
    {
        _stateMachine.Unload(false);
        _stateMachine.Load(settingsState);
    }

    private void OnSelectionSceneOpen(SelectionSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        selectionState.Id = signal.getId();
        selectionState.Name = signal.getName();
        _stateMachine.Load(selectionState);
    }

    private void OnLoadSceneOpen(LoadSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        loadState.ModelId = signal.getId();
        loadState.Name = signal.getName();
        _stateMachine.Load(loadState);
    }

    private void OnMainSceneOpen(MainSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        mainState.Bundle = signal.getBundle();
        mainState.Id = signal.getId();
        mainState.Name = signal.getName();
        _stateMachine.Load(mainState);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<MainSceneOpenSignal>(OnMainSceneOpen);
        _signalBus.Unsubscribe<LoadSceneOpenSignal>(OnLoadSceneOpen);
        _signalBus.Unsubscribe<SettingsSceneOpenSignal>(OnSettingsSceneOpen);
        _signalBus.Unsubscribe<SelectionSceneOpenSignal>(OnSelectionSceneOpen);
        _signalBus.Unsubscribe<StartSceneOpenSignal>(OnStartSceneOpen);
        _signalBus.Unsubscribe<BackButtonPressedSignal>(OnBackButtonPressed);
    }

    public void Tick()
    {
    }
}

