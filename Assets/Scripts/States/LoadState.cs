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

    //private int time = 3;

    public override void Load()
    {
        base.Load();
        loadScreen.Show();
        loadScreen.SetTitle("Загрузка");
        //loadScreen.StartCoroutine(Timer_Elapsed());
        //AssetBundle bundle = null;
        loadScreen.StartCoroutine(AssetBundleHelper.GetAssetBundle(ModelId, value => OnGetAssetBundleCompleted(value)));
        //loadScreen.StartCoroutine(AssetBundleHelper.DownloadBundleNew(ModelId, value => OnGetAssetBundleCompleted(value)));
        //AssetBundleHelper.DownloadBundleSimple(ModelId, value => OnGetAssetBundleCompleted(value));
    }

    private void OnGetAssetBundleCompleted(AssetBundle bundle)
    {
        signalBus.Fire(new MainSceneOpenSignal(bundle, ModelId, Name));
    }

private IEnumerator Timer_Elapsed()
    {
        yield return new WaitForSeconds(2);
        deviceModel.id = ModelId;
        signalBus.Fire<MainSceneOpenSignal>();
    }

    public override void Unload()
    {
        base.Unload();
        loadScreen.Hide();      
    }
}
