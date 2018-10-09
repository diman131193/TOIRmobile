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

    public int ModelId { get; set; }

    private int time = 3;

    public override void Load()
    {
        base.Load();
        loadScreen.Show();
        loadScreen.SetTitle("Загрузка");
        //loadScreen.StartCoroutine(Timer_Elapsed());
        AssetBundle bundle = null;
        loadScreen.StartCoroutine(AssetBundleHelper.GetAssetBundle(ModelId, value => OnGetAssetBundleCompleted(value)));

    }

    private void OnGetAssetBundleCompleted(AssetBundle bundle)
    {
        signalBus.Fire(new MainSceneOpenSignal() {bundle = bundle});
//=======
//        var prefab = bundle.LoadAsset<GameObject>("rolik.prefab");
//        prefab.transform.position = new Vector3(6.5f, 0.0f, 20.0f);
//        deviceModel.Object = prefab;
//        deviceModel.id = 1;
//        signalBus.Fire<MainSceneOpenSignal>();
//>>>>>>> f957a30a52a307a94b8d30738e1afbd1975a7576
    }

    //IEnumerator GetAssetBundle()
    //{
    //    UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("http://10.10.47.201/toir/api/values/getbundle/" + ModelId + "?device=" + Application.platform.ToString()); 
    //    yield return www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
    //    {
    //        Debug.Log(www.error);
    //        signalBus.Fire<SelectionSceneOpenSignal>();
    //    }
    //    else
    //    {
    //        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
    //        //var prefab = bundle.LoadAsset<GameObject>("Combain");
    //        var prefab = bundle.LoadAsset<GameObject>("Cube" + ModelId);
    //        //Instantiate(prefab);
    //        deviceModel.Object = prefab;
    //        deviceModel.Name = "Cube " + ModelId;
    //        deviceModel.Description = "awdaawdawd";
    //        bundle.Unload(false);
    //        signalBus.Fire<MainSceneOpenSignal>();
    //    }

//}

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
