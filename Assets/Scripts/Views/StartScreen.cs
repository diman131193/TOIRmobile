using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : BaseScreen
{
    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private Button SearchButton;

    [SerializeField]
    private Button SettingsButton;
    

    public event Action SettingsClicked = delegate { };
    public event Action StartClicked = delegate { };
    public event Action SearchClicked = delegate { };

    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            StartClicked();
        });

        SearchButton.onClick.AddListener(() =>
        {
            SearchClicked();
        });

        SettingsButton.onClick.AddListener(() =>
        {
            SettingsClicked();
        });

    }

    private void OnDestroy()
    {
        StartButton.onClick.RemoveAllListeners();
        SettingsButton.onClick.RemoveAllListeners();
        SearchButton.onClick.RemoveAllListeners();
    }
}
