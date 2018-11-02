using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Linq;

public class Layout : MonoBehaviour
{
    [SerializeField]
    private Button homeButton;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Text titleText;
    [Inject]
    private SignalBus signalBus;

    void Start()
    {
        homeButton.onClick.AddListener(() =>
        {
            signalBus.Fire<StartSceneOpenSignal>();
        });

        backButton.onClick.AddListener(() =>
        {
            signalBus.Fire<BackButtonPressedSignal>();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            signalBus.Fire<BackButtonPressedSignal>();
        }
    }

    public void SetTitle(string text)
    {
        titleText.text = text;
    }

    private void OnDestroy()
    {
        homeButton.onClick.RemoveAllListeners();
    }
}