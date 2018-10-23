using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class ShopSelectionState : BaseState {
    [Inject]
    public ShopSelectionScreen shopSelectionScreen;
    [Inject]
    private SignalBus signalBus;

    private ShopModel[] dataModel;

    ShopModel[] GetShops()
    {
        ShopModel[] tmpShop = new ShopModel[10]; 
        for (int i = 0; i < 10; i++)
        {
            tmpShop[i] = new ShopModel("" + i, "Shop" + i);
        }
        return tmpShop;
    }

    public override void Load()
    {
        base.Load();
        dataModel = GetShops();
        shopSelectionScreen.SetTitle("ТЧМС");
        shopSelectionScreen.ShopButtonClicked += OnShopButtonClicked;
        shopSelectionScreen.RenderScreenContent(dataModel);
               
        shopSelectionScreen.Show();
    }

    public override void Unload()
    {
        base.Unload();
        shopSelectionScreen.Hide();
    }

    private void OnShopButtonClicked(CustomButton button)
    {
        signalBus.Fire(new SectorSceneOpenSignal(button.getId(), button.getName()));
    }
}
