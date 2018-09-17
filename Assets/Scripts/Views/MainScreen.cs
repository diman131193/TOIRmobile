using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : BaseScreen 
{
    [SerializeField]
    public Button ButtonUp;

    [SerializeField]
    public Button ButtonDown;

    [SerializeField]
    public Text Prompt;

    public event Action ButtonUpClicked = delegate { };
    public event Action ButtonDownClicked = delegate { };

    void Start()
    {
        ButtonUp.onClick.AddListener(()=>
        {
            ButtonUpClicked();
        });

        ButtonDown.onClick.AddListener(() =>
        {
            ButtonDownClicked();
        });

        ButtonDown.interactable = false;
    }

    private void OnDestroy()
    {
        ButtonUp.onClick.RemoveAllListeners();
        ButtonDown.onClick.RemoveAllListeners();
    }
}
