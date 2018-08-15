using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIcon : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	    if (isActiveAndEnabled)
        {
            transform.Rotate(Vector3.forward, 0.5f);
        }
	}
}
