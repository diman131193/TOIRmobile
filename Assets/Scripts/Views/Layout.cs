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
    private Text titleText;
    [Inject]
    private SignalBus signalBus;

    void Start()
    {
        //homeButton = GetComponentInChildren<Button>();
        //titleText = GetComponentsInChildren<Text>().Where(x => x.name == "TitleText").FirstOrDefault();
        homeButton.onClick.AddListener(() =>
        {
            signalBus.Fire<StartSceneOpenSignal>();
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