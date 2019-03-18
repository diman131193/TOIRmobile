
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

        public static IEnumerator DownloadBundle(string model_id, System.Action<AssetBundle> result)
        {
            while (!Caching.ready) yield return null;
            string address = @"file://10.10.47.201/android/" + model_id + ".unity3d";
            Debug.Log(address);
            WWW www = WWW.LoadFromCacheOrDownload(address, 0);
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                yield break;
            }
            Debug.Log("done!");
            AssetBundle bundle = www.assetBundle;
            result.Invoke(bundle);

            www.Dispose();
        }

        public static IEnumerator DownloadBundleNew(string model_id, System.Action<AssetBundle> result)
        {
            string address = @"file://10.10.47.201/android/" + model_id + ".unity3d";
            Debug.Log(address);
            using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(address, 0, 0))
            {
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
        }


        public static void DownloadBundleSimple(string model_id, System.Action<AssetBundle> result)
        {
            string address = "//10.10.47.201/android/" + model_id + ".unity3d";
            Debug.Log(address);
            AssetBundle bundle = AssetBundle.LoadFromFile(address);
            result.Invoke(bundle);
        }


    }
}
