using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BaseScreen : MonoBehaviour {

    [Inject]
    private Layout layout;

    public bool IsShowed { get { return gameObject.activeSelf; } }


	public virtual void Hide () {
        gameObject.SetActive(false);
	}
	
	public virtual void Show () {
        gameObject.SetActive(true);
	}

    public void SetTitle(string text)
    {
        layout.SetTitle(text);
    }

    public virtual void RenderScreenContent()
    {
    }
}
