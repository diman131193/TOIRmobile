using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
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
                Debug.Log(wrapper);
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
                Debug.Log(address);
            }   

            UnityWebRequest www = UnityWebRequest.Get(address);
            yield return www.SendWebRequest();

            //SelectionModel[] res;

            if (www.isNetworkError || www.isHttpError || www.responseCode == 204)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("{\"Items\":" + www.downloadHandler.text + "}");
                SelectionModel[] res = JsonHelper.FromJson<SelectionModel>("{\"Items\":" + www.downloadHandler.text + "}");
                Debug.Log(res);
                result.Invoke(res);
            }
            www.Dispose();
        }
    }
}
