
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

        public static IEnumerator GetAssetBundle(int model_id, System.Action<AssetBundle> result)
        {
            string address = null;//ConfigurationManager.GetComponent("AssetBundleServer"); не работает на устройстве....
            if (address == null)
            {
                address = @"http://10.10.47.201/toir/api/values/getbundle/";
            }
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(address + model_id + "?device=" + Application.platform.ToString());
            yield return www.SendWebRequest();
            AssetBundle bundle = null;
            if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
            {
                Debug.Log(www.error);
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(www);
                result.Invoke(bundle);
            }
            www.Dispose();
        }

        private static IEnumerator GetWebRequest(UnityWebRequest www, System.Action<AssetBundle> result)
        {
            AssetBundle bundle = null;
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
            {
                Debug.Log(www.error);
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(www);
                result.Invoke(bundle);
            }
        }
    }
}
