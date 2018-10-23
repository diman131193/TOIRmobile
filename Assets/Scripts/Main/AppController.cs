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
    private ShopSelectionState shopState;
    [Inject]
    private SectorSelectionState sectorState;
    [Inject]
    private UnitSelectionState unitState;
    [Inject]
    private NodeSelectionState nodeState;
    [Inject]
    private AssemblingSelectionState assemblingState;



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

    [Inject]
    private BillsState billsState;

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
        _signalBus.Subscribe<ShopSceneOpenSignal>(OnShopSceneOpen);
        _signalBus.Subscribe<SectorSceneOpenSignal>(OnSectorSceneOpen);
        _signalBus.Subscribe<UnitSceneOpenSignal>(OnUnitSceneOpen);
        _signalBus.Subscribe<NodeSceneOpenSignal>(OnNodeSceneOpen);
        _signalBus.Subscribe<AssemblingSceneOpenSignal>(OnAssemblingSceneOpen);

        _signalBus.Subscribe<MainSceneOpenSignal>(OnMainSceneOpen);
        _signalBus.Subscribe<LoadSceneOpenSignal>(OnLoadSceneOpen);
        _signalBus.Subscribe<SettingsSceneOpenSignal>(OnSettingsSceneOpen);
        _signalBus.Subscribe<SelectionSceneOpenSignal>(OnSelectionSceneOpen);
        _signalBus.Subscribe<StartSceneOpenSignal>(OnStartSceneOpen);
        _signalBus.Subscribe<BillsSceneOpenSignal>(OnBillsSceneOpen);
        _signalBus.Subscribe<BackButtonPressedSignal>(OnBackButtonPressed);
        _signalBus.Fire<StartSceneOpenSignal>();
    }

    private void OnShopSceneOpen()
    {
        _stateMachine.Unload(false);
        _stateMachine.Load(shopState);
    }

    private void OnSectorSceneOpen(SectorSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        sectorState.Id = signal.getId();
        sectorState.Name = signal.getName();
        _stateMachine.Load(sectorState);
    }

    private void OnUnitSceneOpen(UnitSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        unitState.Id = signal.getId();
        unitState.Name = signal.getName();
        _stateMachine.Load(unitState);
    }

    private void OnNodeSceneOpen(NodeSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        nodeState.Id = signal.getId();
        nodeState.Name = signal.getName();
        _stateMachine.Load(nodeState);
    }

    private void OnAssemblingSceneOpen(AssemblingSceneOpenSignal signal)
    {
        _stateMachine.Unload(false);
        assemblingState.Id = signal.getId();
        assemblingState.Name = signal.getName();
        _stateMachine.Load(assemblingState);
    }

    private void OnBillsSceneOpen()
    {
        _stateMachine.Unload(false);
        _stateMachine.Load(billsState);
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

    private void OnSelectionSceneOpen()
    {
        _stateMachine.Unload(false);
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
        _signalBus.Unsubscribe<ShopSceneOpenSignal>(OnShopSceneOpen);
        _signalBus.Unsubscribe<SectorSceneOpenSignal>(OnSectorSceneOpen);
        _signalBus.Unsubscribe<UnitSceneOpenSignal>(OnUnitSceneOpen);
        _signalBus.Unsubscribe<NodeSceneOpenSignal>(OnNodeSceneOpen);
        _signalBus.Unsubscribe<AssemblingSceneOpenSignal>(OnAssemblingSceneOpen);

        _signalBus.Unsubscribe<MainSceneOpenSignal>(OnMainSceneOpen);
        _signalBus.Unsubscribe<LoadSceneOpenSignal>(OnLoadSceneOpen);
        _signalBus.Unsubscribe<SettingsSceneOpenSignal>(OnSettingsSceneOpen);
        _signalBus.Unsubscribe<SelectionSceneOpenSignal>(OnSelectionSceneOpen);
        _signalBus.Unsubscribe<StartSceneOpenSignal>(OnStartSceneOpen);
        _signalBus.Unsubscribe<BillsSceneOpenSignal>(OnBillsSceneOpen);
        _signalBus.Unsubscribe<BackButtonPressedSignal>(OnBackButtonPressed);
    }

    public void Tick()
    {
    }
}

