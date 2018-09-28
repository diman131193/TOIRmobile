using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : BaseScreen 
{
    [SerializeField]
    public GameObject ChartPanel;

    [SerializeField]
    public Button ButtonUp;

    [SerializeField]
    public Button ButtonDown;

    [SerializeField]
    public Button ButtonChart;

    [SerializeField]
    public Button ButtonClose;

    [SerializeField]
    public Text Prompt;

    public event Action ButtonUpClicked = delegate { };
    public event Action ButtonDownClicked = delegate { };
    public event Action ButtonChartClicked = delegate { };
    public event Action ButtonCloseClicked = delegate { };

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

        ButtonChart.onClick.AddListener(() =>
        {
            ButtonChartClicked();
        });

        ButtonClose.onClick.AddListener(() =>
        {
            ButtonCloseClicked();
        });

        ButtonDown.interactable = false;
    }

    private void OnDestroy()
    {
        ButtonUp.onClick.RemoveAllListeners();
        ButtonDown.onClick.RemoveAllListeners();
        ButtonChart.onClick.RemoveAllListeners();
        ButtonClose.onClick.RemoveAllListeners();
    }
}
