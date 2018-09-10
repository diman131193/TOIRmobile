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
    private Text _title;

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

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    private void OnDestroy()
    {
        ButtonUp.onClick.RemoveAllListeners();
        ButtonDown.onClick.RemoveAllListeners();
    }
}
