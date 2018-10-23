using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class UnitSelectionState : BaseState {

    public string Id;
    public string Name;

    [Inject]
    public UnitSelectionScreen unitSelectionScreen;
    [Inject]
    private SignalBus signalBus;

    private UnitModel[] dataModel;

    UnitModel[] GetUnits()
    {
        UnitModel[] tmpUnit = new UnitModel[10];
        for (int i = 0; i < 10; i++)
        {
            tmpUnit[i] = new UnitModel("" + i, "Unit" + Id + "_" + i);
        }
        return tmpUnit;
    }

    public override void Load()
    {
        base.Load();
        dataModel = GetUnits();
        unitSelectionScreen.SetTitle(Name);
        unitSelectionScreen.UnitButtonClicked += OnUnitButtonClicked;
        unitSelectionScreen.RenderScreenContent(dataModel);

        unitSelectionScreen.Show();
    }

    public override void Unload()
    {
        base.Unload();
        unitSelectionScreen.Hide();
    }

    private void OnUnitButtonClicked(CustomButton button)
    {
        signalBus.Fire(new NodeSceneOpenSignal(button.getId(), button.getName()));
    }

}
