using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public int SelectedId;

	void Start () {
        Transform[] childArray = GetComponentsInChildren<Transform>();

        for (var i = 1; i < childArray.Length; i++)
        {
            childArray[i].gameObject.AddComponent<OnMouseClick>();
            childArray[i].gameObject.GetComponent<OnMouseClick>().model = this;
        }
    }
}
