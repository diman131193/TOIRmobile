using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;
using System;
using System.IO;

public class SelectionState : BaseState
{

    public string Id;
    public string Name;

    private int Count_2DClick = 0;

    [Inject]
    public SelectionScreen selectionScreen;
    [Inject]
    public SignalBus signalBus;

    private SelectionModel[] dataModel;

    void GetSelections(SelectionModel[] res)
    {
        selectionScreen.RenderScreenContent(res); 
    }

    public override void Load()
    {
        base.Load();
        selectionScreen.ClearEntity();
        selectionScreen.ClearCharts();
        Count_2DClick = 0;
        selectionScreen.ChartPanelOpen = false;
        selectionScreen._2D.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        selectionScreen.SetTitle(Name);
        selectionScreen.ChartButtonClicked += OnChartButtonClicked;
        selectionScreen.RenderChartContent();
        selectionScreen.SelectionButtonClicked += OnSelectionButtonClicked;
        selectionScreen._3DButtonClicked += On_3DButtonClicked;
        selectionScreen._2DButtonClicked += On_2DButtonClicked;
        selectionScreen.Show();
        selectionScreen.StartCoroutine(Helpers.EntityHelper.getEntityList(Id, value => GetSelections(value)));
    }

    public override void Unload()
    {
        base.Unload();
        selectionScreen.SelectionButtonClicked -= OnSelectionButtonClicked;
        selectionScreen.ChartButtonClicked -= OnChartButtonClicked;
        selectionScreen._3DButtonClicked -= On_3DButtonClicked;
        selectionScreen._2DButtonClicked -= On_2DButtonClicked;
        selectionScreen.Hide();
    }

    private void OnSelectionButtonClicked(CustomButton button)
    {
        signalBus.Fire(new SelectionSceneOpenSignal(button.getId(), button.getName()));
    }

    IEnumerator OpenPDF()
    {
        Debug.Log("OpenPDF");
        string path;
        string savePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = "jar:file://" + Application.dataPath + "!/assets" + "/72_167_01_a1_1a_rus1123.dwg_result.pdf";
            savePath = Application.persistentDataPath;
        }
        else
        {
            path = "file://" + Application.streamingAssetsPath + "/72_167_01_a1_1a_rus1123.dwg_result.pdf";
            savePath = "C:/Users/markin/TOIR";
        }
        WWW www = new WWW(path);
        yield return www;
        Debug.Log(savePath);
        string error = www.error;
        Debug.Log(error);
        byte[] bytes = www.bytes;
        string result = "Done, bytes downloaded, File size : " + bytes.Length;
        Debug.Log(result);
        try
        {
            File.WriteAllBytes(savePath + "/72_167_01_a1_1a_rus1123.dwg_result.pdf", bytes);
            error = "No Errors so far";
            Debug.Log(error);
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Debug.Log(error);
        }
        Application.OpenURL(savePath + "/72_167_01_a1_1a_rus1123.dwg_result.pdf");
    }

    private void OnChartButtonClicked(CustomButton button)
    {
        selectionScreen.StartCoroutine(OpenPDF());
    }

    private void On_3DButtonClicked()
    {
        signalBus.Fire(new LoadSceneOpenSignal("1", "Ролик"));
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
