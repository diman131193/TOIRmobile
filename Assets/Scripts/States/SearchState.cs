using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SearchState : BaseState {

    [Inject]
    public SearchScreen searchScreen { get; private set; }

    [Inject]
    private SignalBus signalBus;

    private string name;

    public override void Load()
    {
        base.Load();
        searchScreen.SearchClicked += OnSearchClicked;
        searchScreen.SetTitle("Поиск");
        searchScreen.Show();
    }
    public override void Unload()
    {
        base.Unload();
        searchScreen.SearchClicked -= OnSearchClicked;
        searchScreen.Hide();
    }

    public void OnSearchClicked()
    {
        name = searchScreen.GetName();

        if (name != "")
        {
            signalBus.Fire(new ResultSceneOpenSignal(name));
        }
    }

}
