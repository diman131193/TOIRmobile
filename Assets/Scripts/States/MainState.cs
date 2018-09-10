﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainState : BaseState {

    [Inject]
    public MainScreen mainScreen { get; private set; }
    [Inject]
    private SignalBus signalBus;
    [Inject]
    private DeviceModel deviceModel;

    private GameObject model;

    public override void Load()
    {
        base.Load();
        mainScreen.SetTitle(deviceModel.Name);
        model = GameObject.Instantiate(deviceModel.Object);
        mainScreen.ButtonUpClicked += ButtonUpClicked;
        mainScreen.ButtonDownClicked += ButtonDownClicked;
        mainScreen.GetComponent<ModelRotation>().Model = model.transform;
        mainScreen.GetComponent<Zoom>().Model = model.transform;
        mainScreen.Show();
    }
    public override void Unload()
    {
        base.Unload();
        mainScreen.ButtonUpClicked -= ButtonUpClicked;
        mainScreen.ButtonDownClicked -= ButtonDownClicked;
        GameObject.Destroy(model);
        mainScreen.Hide();
    }

    public void ButtonUpClicked()
    {
        Debug.Log("ButtonClick MenuSceneOpenSignal");
        signalBus.Fire<SelectionSceneOpenSignal>();
    }

    public void ButtonDownClicked()
    {
        Debug.Log("ButtonClick MenuSceneOpenSignal");
        signalBus.Fire<SelectionSceneOpenSignal>();
    }
}
