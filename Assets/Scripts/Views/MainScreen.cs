using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : BaseScreen 
{
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Text _title;

    public event Action SettingsClicked = delegate { };

    void Start()
    {
        _settingsButton.onClick.AddListener(()=>
        {
            SettingsClicked();
        });
    }

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    private void OnDestroy()
    {
        _settingsButton.onClick.RemoveAllListeners();
    }
}
