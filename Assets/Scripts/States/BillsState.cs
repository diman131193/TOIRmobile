using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BillsState : BaseState
{

    [Inject]
    public BillsScreen billsScreen { get; private set; }

    [Inject]
    private SignalBus signalBus;

    public override void Load()
    {
        base.Load();
        billsScreen.SetTitle("Заявки");
        billsScreen.Show();
    }
    public override void Unload()
    {
        base.Unload();
        billsScreen.Hide();
    }
}