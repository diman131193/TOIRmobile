﻿using System.Collections;
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

    private int time = 3;

    public override void Load()
    {
        base.Load();
        loadScreen.Show();
        loadScreen.StartCoroutine(GetAssetBundle());
    }

    IEnumerator GetAssetBundle()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("http://localhost:50442/api/values/getbundle");
        yield return www.Send();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            //var prefab = bundle.LoadAsset<GameObject>("Combain");
            var prefab = bundle.LoadAsset<GameObject>("Cube");
            //Instantiate(prefab);
            deviceModel.Object = prefab;
            deviceModel.Name = "New";
            deviceModel.Description = "awdaawdawd";
            bundle.Unload(false);
        }
        signalBus.Fire<MainSceneOpenSignal>();
    }

    private IEnumerator Timer_Elapsed()
    {
        yield return new WaitForSeconds(3);
        signalBus.Fire<MainSceneOpenSignal>();
    }

    public override void Unload()
    {
        base.Unload();
        loadScreen.Hide();      
    }
}
