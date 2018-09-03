using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour {

    public bool IsShowed { get { return gameObject.activeSelf; } }

	public virtual void Hide () {
        gameObject.SetActive(false);
	}
	
	public virtual void Show () {
        gameObject.SetActive(true);
	}

    public virtual void RenderScreenContent()
    {
    }
}
