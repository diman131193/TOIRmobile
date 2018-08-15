using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuState : BaseState {

    [Inject]
    public MenuScreen menuScreen { get; private set; }
    [Inject]
    private SignalBus signalBus;
    public override void Load()
    {
        base.Load();
        Debug.Log("ButtonClick MainSceneOpenSignal");
        menuScreen.SettingsClicked += OnSettingsClicked;
        menuScreen.SetTitle("IT IS MENU!!!");
        menuScreen.Show();
    }
    public override void Unload()
    {
        base.Unload();
        menuScreen.SettingsClicked -= OnSettingsClicked;
        menuScreen.Hide();
    }

    public void OnSettingsClicked()
    {
        Debug.Log("ButtonClick MainSceneOpenSignal");
        signalBus.Fire<LoadSceneOpenSignal>();
    }

}
