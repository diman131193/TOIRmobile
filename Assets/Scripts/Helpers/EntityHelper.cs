using System.Runtime.Serialization;
using System.Linq;
using Assets.Scripts.Helpers;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using System;

namespace Helpers
{
    class EntityHelper
    {
        public static class JsonHelper
        {
            public static T[] FromJson<T>(string json)
            {
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
                return wrapper.Items;
            }

            public static string ToJson<T>(T[] array)
            {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper);
            }

            public static string ToJson<T>(T[] array, bool prettyPrint)
            {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper, prettyPrint);
            }

            [Serializable]
            private class Wrapper<T>
            {
                public T[] Items;
            }
        }

        public static IEnumerator getEntityList(string id, System.Action<SelectionModel[]> result)
        {
            string address = @"http://10.10.47.201/toirsvc/api/getEntityList/";
            
            if (id != null)
            {
                address += id;
            }   

            UnityWebRequest www = UnityWebRequest.Get(address);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
            {
                Debug.Log(www.error);
            }
            else
            {
                SelectionModel[] res = JsonHelper.FromJson<SelectionModel>("{\"Items\":" + www.downloadHandler.text + "}");
                result.Invoke(res);
            }
            www.Dispose();
        }

        public static IEnumerator getInstructions(string id, System.Action<InstructionModel[]> result)
        {
            string address = @"http://10.10.47.201/toirsvc/api/getInstructions/";

            if (id != null)
            {
                address += id;
            }

            UnityWebRequest www = UnityWebRequest.Get(address);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
            {
                Debug.Log(www.error);
            }
            else
            {
                InstructionModel[] res = JsonHelper.FromJson<InstructionModel>("{\"Items\":" + www.downloadHandler.text + "}");
                result.Invoke(res);
            }
            www.Dispose();
        }

        public static IEnumerator doSearch(string id, System.Action<SelectionModel[]> result)
        {
            string address = @"http://10.10.47.201/toirsvc/api/doSearch/";

            if (id != null)
            {
                address += id;
            }

            UnityWebRequest www = UnityWebRequest.Get(address);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
            {
                Debug.Log(www.error);
            }
            else
            {
                SelectionModel[] res = JsonHelper.FromJson<SelectionModel>("{\"Items\":" + www.downloadHandler.text + "}");
                result.Invoke(res);
            }
            www.Dispose();
        }

        public static IEnumerator getCharts(string id, System.Action<ChartsModel[]> result)
        {
            string address = @"http://10.10.47.201/toirsvc/api/getCharts/";

            if (id != null)
            {
                address += id;
            }

            UnityWebRequest www = UnityWebRequest.Get(address);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
            {
                Debug.Log(www.error);
            }
            else
            {
                ChartsModel[] res = JsonHelper.FromJson<ChartsModel>("{\"Items\":" + www.downloadHandler.text + "}");
                result.Invoke(res);
            }
            www.Dispose();
        }
    }
}
