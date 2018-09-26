using System;
using UnityEngine;
using UnityEngine.UI;

public class BillsScreen : BaseScreen
{
    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private Button SettingsButton;

    [SerializeField]
    private Button BillsButton;

    public event Action SettingsClicked = delegate { };
    public event Action BillsClicked = delegate { };
    public event Action StartClicked = delegate { };

    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            StartClicked();
        });

        SettingsButton.onClick.AddListener(() =>
        {
            SettingsClicked();
        });

        BillsButton.onClick.AddListener(() =>
        {
            BillsClicked();
        });
    }

    private void OnDestroy()
    {
        StartButton.onClick.RemoveAllListeners();
        BillsButton.onClick.RemoveAllListeners();
        SettingsButton.onClick.RemoveAllListeners();
    }
}
