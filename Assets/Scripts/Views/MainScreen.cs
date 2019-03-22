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
    public Button ButtonSettings;

    [SerializeField]
    public Button ButtonInstrument;

    [SerializeField]
    public Button ButtonClock;

    [SerializeField]
    public Text Description;

    [SerializeField]
    public Text OperationType;

    [SerializeField]
    public GameObject mainPanel;

    public event Action ButtonUpClicked = delegate { };
    public event Action ButtonDownClicked = delegate { };
    public event Action ButtonSettingsClicked = delegate { };
    public event Action ButtonInstrumentClicked = delegate { };
    public event Action ButtonClockClicked = delegate { };

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

        ButtonSettings.onClick.AddListener(() =>
        {
            ButtonSettingsClicked();
        });

        ButtonInstrument.onClick.AddListener(() =>
        {
            ButtonInstrumentClicked();
        });

        ButtonClock.onClick.AddListener(() =>
        {
            ButtonClockClicked();
        });
    }

    private void OnDestroy()
    {
        ButtonUp.onClick.RemoveAllListeners();
        ButtonDown.onClick.RemoveAllListeners();
        ButtonSettings.onClick.RemoveAllListeners();
        ButtonInstrument.onClick.RemoveAllListeners();
        ButtonClock.onClick.RemoveAllListeners();
    }
}
