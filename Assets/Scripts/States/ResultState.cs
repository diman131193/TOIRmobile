using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;
using System;
using System.IO;

public class ResultState : BaseState
{

    public string Name;

    public SelectionModel rolik = new SelectionModel("31-02-01-002-003-004-008", "Ролик в сборе №3 теплоизолированного отводящего рольганга механизма выдачи заготовки из печи");
    public SelectionModel cylinder = new SelectionModel("31-02-09-101-001-002-035", "Пневмоцилиндр CSV2AN2S33AC-M22");

    public SelectionModel[] temp = new SelectionModel[2];
    
    [Inject]
    public ResultScreen resultScreen;

    [Inject]
    public SignalBus signalBus;

    private SelectionModel[] dataModel;

    void GetSelections(SelectionModel[] res)
    {
        resultScreen.RenderScreenContent(res); 
    }

    public override void Load()
    {
        base.Load();
        resultScreen.ClearEntity();
        resultScreen.SetTitle("Результат поиска по запросу \"" + Name + "\"" );
        resultScreen.SelectionButtonClicked += OnSelectionButtonClicked;
        resultScreen.Show();
        //resultScreen.StartCoroutine(Helpers.EntityHelper.getEntityList(Name, value => GetSelections(value)));
        temp[0] = rolik;
        temp[1] = cylinder;
        resultScreen.RenderScreenContent(temp);
    }

    public override void Unload()
    {
        base.Unload();
        resultScreen.SelectionButtonClicked -= OnSelectionButtonClicked;
        resultScreen.Hide();
    }

    private void OnSelectionButtonClicked(CustomButton button)
    {
        signalBus.Fire(new SelectionSceneOpenSignal(button.getId(), button.getName()));
    }
}
