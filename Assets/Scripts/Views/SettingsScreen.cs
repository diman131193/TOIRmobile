using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : BaseScreen 
{

    [SerializeField]
    private Button LoginButton;

    [SerializeField]
    private InputField Login;

    [SerializeField]
    private InputField Password;

    public event Action SettingsClicked = delegate { };

    public string GetLogin()
    {
        return Login.text;
    }

    public string GetPassword()
    {
        return Password.text;
    }

    void Start()
    {
        LoginButton.onClick.AddListener(() =>
        {
            SettingsClicked();
        });
    }

    private void OnDestroy()
    {
        LoginButton.onClick.RemoveAllListeners();
    }
}
