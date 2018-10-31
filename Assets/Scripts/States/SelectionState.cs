using UnityEngine;
using UnityEngine.UI;
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
        selectionScreen._3DButtonClicked += On_3DButtonClicked;
        selectionScreen._2DButtonClicked += On_2DButtonClicked;
        selectionScreen.RenderScreenContent(dataModel);

        selectionScreen.Show();
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
            ColorBlock cb = selectionScreen._2D.colors;
            cb.normalColor = new Color(200f, 200f, 200f, 255f);
            selectionScreen._2D.colors = cb;
        }
        else
        {
            selectionScreen.ChartPanelOpen = false;
            ColorBlock cb = selectionScreen._2D.colors;
            cb.normalColor = new Color(255f, 255f, 255f, 255f);
            selectionScreen._2D.colors = cb;
        }
    }

}
