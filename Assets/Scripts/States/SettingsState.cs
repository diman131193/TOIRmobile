using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SettingsState : BaseState {

    [Inject]
    public SettingsScreen settingsScreen { get; private set; }

    [Inject]
    private SignalBus signalBus;

    private string login;

    private string password;

    public override void Load()
    {
        base.Load();
        settingsScreen.SettingsClicked += OnSettingsClicked;
        settingsScreen.Show();
    }
    public override void Unload()
    {
        base.Unload();
        settingsScreen.SettingsClicked -= OnSettingsClicked;
        settingsScreen.Hide();
    }

    public void OnSettingsClicked()
    {
        login = settingsScreen.GetLogin();
        password = settingsScreen.GetPassword();

        if (login != "" && password != "")
        {
            signalBus.Fire<StartSceneOpenSignal>();
        }
    }

}
