using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Screen1State : BaseState {

    [Inject]
    public Screen1Screen screen1Screen { get; private set; }

    [Inject]
    private SignalBus signalBus;

    private string login;

    private string password;

    public override void Load()
    {
        base.Load();
        screen1Screen.SettingsClicked += OnSettingsClicked;
        screen1Screen.Show();
    }
    public override void Unload()
    {
        base.Unload();
        screen1Screen.SettingsClicked -= OnSettingsClicked;
        screen1Screen.Hide();
    }

    public void OnSettingsClicked()
    {
        login = screen1Screen.GetLogin();
        password = screen1Screen.GetPassword();

        if (login != "" && password != "")
        {
            signalBus.Fire<Screen2SceneOpenSignal>();
        }
    }

}
