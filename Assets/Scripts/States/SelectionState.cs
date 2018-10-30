using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class SelectionState : BaseState
{

    public string Id;
    public string Name;

    [Inject]
    public SelectionScreen selectionScreen;
    [Inject]
    private SignalBus signalBus;

    private SelectionModel[] dataModel;

    SelectionModel[] GetSelections()
    {
        SelectionModel[] tmpSelection = new SelectionModel[10];
        for (int i = 0; i < 10; i++)
        {
            tmpSelection[i] = new SelectionModel("" + i, "Selection" + Id + "_" + i);
        }
        return tmpSelection;
    }

    public override void Load()
    {
        base.Load();
        dataModel = GetSelections();
        selectionScreen.SetTitle(Name);
        selectionScreen.SelectionButtonClicked += OnSelectionButtonClicked;

        selectionScreen.RenderScreenContent(dataModel);

        selectionScreen.Show();
    }

    public override void Unload()
    {
        base.Unload();
        selectionScreen.SelectionButtonClicked -= OnSelectionButtonClicked;
        selectionScreen.Hide();
    }

    private void OnSelectionButtonClicked(CustomButton button)
    {
        signalBus.Fire(new SelectionSceneOpenSignal(button.getId(), button.getName()));
    }

}
