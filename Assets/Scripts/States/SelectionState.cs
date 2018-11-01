using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;

public class SelectionState : BaseState
{

    public string Id;
    public string Name;

    private int Count_2DClick = 0;

    [Inject]
    public SelectionScreen selectionScreen;
    [Inject]
    private SignalBus signalBus;

    private SelectionModel[] dataModel;

    void GetSelections(SelectionModel[] res)
    {
        //SelectionModel[] tmpSelection = new SelectionModel[10];
        //for (int i = 0; i < 10; i++)
        //{
        //    tmpSelection[i] = new SelectionModel("" + i, "Selection" + Id + "_" + i);
        //}

        //return tmpSelection;
        Debug.Log(res);
        foreach (var elem in res)
        {
            Debug.Log(elem.getId());
        }
        selectionScreen.RenderScreenContent(res);

        
    }

    public override void Load()
    {
        base.Load();
        selectionScreen.ClearEntity();
        selectionScreen.SetTitle(Name);
        selectionScreen.SelectionButtonClicked += OnSelectionButtonClicked;
        selectionScreen._3DButtonClicked += On_3DButtonClicked;
        selectionScreen._2DButtonClicked += On_2DButtonClicked;
        selectionScreen.Show();
        selectionScreen.StartCoroutine(Helpers.EntityHelper.getEntityList(Id, value => GetSelections(value)));
        //selectionScreen.RenderScreenContent(dataModel);

        //selectionScreen.Show();
    }

    public override void Unload()
    {
        base.Unload();
        selectionScreen.SelectionButtonClicked -= OnSelectionButtonClicked;
        selectionScreen._3DButtonClicked -= On_3DButtonClicked;
        selectionScreen._2DButtonClicked -= On_2DButtonClicked;
        selectionScreen.Hide();
    }

    private void OnSelectionButtonClicked(CustomButton button)
    {
        signalBus.Fire(new SelectionSceneOpenSignal(button.getId(), button.getName()));
    }

    private void On_3DButtonClicked()
    {
        signalBus.Fire(new LoadSceneOpenSignal(1, "Ролик прокатный 1"));
    }

    private void On_2DButtonClicked()
    {
        Count_2DClick++;
        
        if (Count_2DClick % 2 == 1)
        {
            selectionScreen.ChartPanelOpen = true;
            selectionScreen._2D.gameObject.GetComponent<Image>().color = new Color32(220, 220, 220, 255);
        }
        else
        {
            selectionScreen.ChartPanelOpen = false;
            selectionScreen._2D.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

}
