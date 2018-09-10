using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : BaseScreen 
{
    [SerializeField]
    private Button ButtonUp;

    [SerializeField]
    private Button ButtonDown;

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
    }

    private void OnDestroy()
    {
        ButtonUp.onClick.RemoveAllListeners();
        ButtonDown.onClick.RemoveAllListeners();
    }
}
