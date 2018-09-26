using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartState : BaseState
{

    [Inject]
    public StartScreen startScreen { get; private set; }

    [Inject]
    private SignalBus signalBus;

    public override void Load()
    {
        base.Load();
        startScreen.StartClicked += OnStartClicked;
        startScreen.SettingsClicked += OnSettingsClicked;
        startScreen.BillsClicked += OnBillsClicked;
        startScreen.SetTitle("ТОиР");
        startScreen.Show();
    }

 
    public override void Unload()
    {
        base.Unload();
        startScreen.StartClicked -= OnStartClicked;
        startScreen.SettingsClicked -= OnSettingsClicked;
        startScreen.Hide();
    }

    public void OnSettingsClicked()
    {
       signalBus.Fire<SettingsSceneOpenSignal>(); 
    }

    public void OnStartClicked()
    {
        signalBus.Fire<SelectionSceneOpenSignal>();
    }

    private void OnBillsClicked()
    {
        signalBus.Fire<BillsSceneOpenSignal>();
    }


}