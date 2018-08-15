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
    private MenuState menuState;
    [Inject]
    private MainState mainState;
    [Inject]
    private LoadState loadState;

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
        _signalBus.Subscribe<MenuSceneOpenSignal>(OnMenuSceneOpen);
        _signalBus.Subscribe<MainSceneOpenSignal>(OnMainSceneOpen);
        _signalBus.Subscribe<LoadSceneOpenSignal>(OnLoadSceneOpen);
        _signalBus.Fire<MenuSceneOpenSignal>();
    }

    private void OnLoadSceneOpen(LoadSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        loadState.ModelId = signal.id;
        _stateMachine.Load(loadState);
    }

    private void OnMenuSceneOpen()
    {
        _stateMachine.Unload(false);
        _stateMachine.Load(menuState);
    }

    private void OnMainSceneOpen()
    {
        _stateMachine.Unload(false);
        _stateMachine.Load(mainState);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<MenuSceneOpenSignal>(OnMenuSceneOpen);
        _signalBus.Unsubscribe<MainSceneOpenSignal>(OnMainSceneOpen);
        _signalBus.Unsubscribe<LoadSceneOpenSignal>(OnLoadSceneOpen);
    }

    public void Tick()
    {
    }
}

