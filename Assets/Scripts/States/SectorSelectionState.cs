using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class SectorSelectionState : BaseState {

    public string Id;
    public string Name;

    [Inject]
    public SectorSelectionScreen sectorSelectionScreen;
    [Inject]
    private SignalBus signalBus;

    private SectorModel[] dataModel;

    SectorModel[] GetSectors()
    {
        SectorModel[] tmpSector = new SectorModel[10];
        for (int i = 0; i < 10; i++)
        {
            tmpSector[i] = new SectorModel("" + i, "Sector" + Id + "_" + i);
        }
        return tmpSector;
    }

    public override void Load()
    {
        base.Load();
        dataModel = GetSectors();
        sectorSelectionScreen.SetTitle(Name);
        sectorSelectionScreen.SectorButtonClicked += OnSectorButtonClicked;

        sectorSelectionScreen.RenderScreenContent(dataModel);

        sectorSelectionScreen.Show();
    }

    public override void Unload()
    {
        base.Unload();
        sectorSelectionScreen.Hide();
    }

    private void OnSectorButtonClicked(CustomButton button)
    {
        signalBus.Fire(new UnitSceneOpenSignal(button.getId(), button.getName()));
    }

}
