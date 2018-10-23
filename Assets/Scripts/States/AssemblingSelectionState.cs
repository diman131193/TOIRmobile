using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class AssemblingSelectionState : BaseState {

    public string Id;
    public string Name;

    [Inject]
    public AssemblingSelectionScreen assemblingSelectionScreen;
    [Inject]
    private SignalBus signalBus;

    private AssemblingModel[] dataModel;

    AssemblingModel[] GetAssemblings()
    {
        AssemblingModel[] tmpAssembling = new AssemblingModel[10];
        for (int i = 0; i < 10; i++)
        {
            tmpAssembling[i] = new AssemblingModel("" + i, "Assembling" + Id + "_" + i);
        }
        return tmpAssembling;
    }

    public override void Load()
    {
        base.Load();
        dataModel = GetAssemblings();
        assemblingSelectionScreen.SetTitle(Name);
        assemblingSelectionScreen.AssemblingButtonClicked += OnAssemblingButtonClicked;
        assemblingSelectionScreen.RenderScreenContent(dataModel);

        assemblingSelectionScreen.Show();
    }

    public override void Unload()
    {
        base.Unload();
        assemblingSelectionScreen.Hide();
    }

    private void OnAssemblingButtonClicked(CustomButton button)
    {
        signalBus.Fire(new LoadSceneOpenSignal(1));
    }
}
