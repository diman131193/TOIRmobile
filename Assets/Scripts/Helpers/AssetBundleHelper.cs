
using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Helpers
{
    public static class AssetBundleHelper
    {

        public static IEnumerator GetAssetBundle(string model_id, System.Action<AssetBundle> result)
        {
            string address = null;//ConfigurationManager.GetComponent("AssetBundleServer"); не работает на устройстве....
            if (address == null)
            {
                //address = @"http://10.10.47.201/toir/api/values/getbundle/";
                address = @"http://10.10.47.201/toirsvc/api/getbundle/";
            }
            //address = address + model_id + "?device=" + Application.platform.ToString();
            address = address + model_id;
            Debug.Log(address);
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(address);
            yield return www.SendWebRequest();
            Debug.Log(www.downloadedBytes);
            AssetBundle bundle = null;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                yield break;
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(www);
                result.Invoke(bundle);
            }
            www.Dispose();
        }
    }
}
