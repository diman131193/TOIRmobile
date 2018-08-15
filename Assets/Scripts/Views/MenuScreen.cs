using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseScreen 
{
    [SerializeField]
    private ScrollRect _scrollRect;
    [SerializeField]
    private Text _title;

    private Button[] buttons;

    public event Action<int> SettingsClicked = delegate { };

    void Start()
    {
        buttons = _scrollRect.GetComponentsInChildren<Button>();
        for (var i = 0; i < buttons.Length; i++)
        {
            var j = i + 1;
            buttons[i].onClick.AddListener(() =>
            {
                SettingsClicked(j);
            });
        }
    }

    public void SetTitle(string title)
    {
        _title.text = title;
        
    }
    private void OnDestroy()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
    }
}
