using System;
using UnityEngine;
using UnityEngine.UI;

public class SearchScreen : BaseScreen 
{

    [SerializeField]
    private Button SearchButton;

    [SerializeField]
    private InputField inputField;

    public event Action SearchClicked = delegate { };

    public string GetName()
    {
        return inputField.text;
    }

    void Start()
    {
        SearchButton.onClick.AddListener(() =>
        {
            SearchClicked();
        });
    }

    private void OnDestroy()
    {
        SearchButton.onClick.RemoveAllListeners();
    }
}
