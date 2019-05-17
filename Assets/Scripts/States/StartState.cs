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
        startScreen.SearchClicked += OnSearchClicked;
        startScreen.SettingsClicked += OnSettingsClicked;
        startScreen.SetTitle("ТОиР");
        startScreen.Show();
    }

 
    public override void Unload()
    {
        base.Unload();
        startScreen.StartClicked -= OnStartClicked;
        startScreen.SettingsClicked -= OnSettingsClicked;
        startScreen.SearchClicked -= OnSearchClicked;
        startScreen.Hide();
    }

    public void OnSettingsClicked()
    {
       signalBus.Fire<SettingsSceneOpenSignal>(); 
    }

    public void OnSearchClicked()
    {
        signalBus.Fire<SearchSceneOpenSignal>();
    }

    public void OnStartClicked()
    {
        signalBus.Fire(new SelectionSceneOpenSignal("31", "Тулачермет-Сталь"));
    }
}