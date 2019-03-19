using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class LoadState : BaseState {

    [Inject]
    public LoadScreen loadScreen { get; private set; }
    [Inject]
    private SignalBus signalBus;
    [Inject]
    private DeviceModel deviceModel;

    public string ModelId { get; set; }
    public string Name;
    

    public override void Load()
    {
        base.Load();
        loadScreen.Show();
        loadScreen.SetTitle("Загрузка");
        loadScreen.StartCoroutine(AssetBundleHelper.GetAssetBundle(ModelId, value => OnGetAssetBundleCompleted(value)));
    }

    private void OnGetAssetBundleCompleted(AssetBundle bundle)
    {
        signalBus.Fire(new MainSceneOpenSignal(bundle, ModelId, Name));
    }

    public override void Unload()
    {
        base.Unload();
        loadScreen.Hide();      
    }
}
