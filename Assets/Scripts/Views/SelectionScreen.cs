using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : BaseScreen
{
    [SerializeField]
    public RectTransform ButtonPrefab;

    [SerializeField]
    public ScrollRect SelectionView;

    [SerializeField]
    public Button _3D;

    [SerializeField]
    public Button _2D;

    [SerializeField]
    public GameObject ChartPanel;

    [SerializeField]
    public GameObject SelectionPanel;

    public bool ChartPanelOpen = false;
    private RectTransform rectChartPanel;
    private RectTransform rectSelectionPanel;
    public float speed = 5f;

    public event Action<CustomButton> SelectionButtonClicked = delegate { };
    public event Action _3DButtonClicked = delegate { };
    public event Action _2DButtonClicked = delegate { };

    void Start()
    {
        _3D.onClick.AddListener(() =>
        {
            _3DButtonClicked();
        });

        _2D.onClick.AddListener(() =>
        {
            _2DButtonClicked();
        });

        rectChartPanel = ChartPanel.GetComponent<RectTransform>();
        rectSelectionPanel = SelectionPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (ChartPanelOpen && rectChartPanel.offsetMin.x != 10)
        {
            rectChartPanel.offsetMin += new Vector2(speed, 0);
            rectChartPanel.offsetMax += new Vector2(speed, 0);
            rectSelectionPanel.offsetMin += new Vector2(speed, 0);
        }
        if (!ChartPanelOpen && rectChartPanel.offsetMax.x != 0)
        {
            rectChartPanel.offsetMin -= new Vector2(speed, 0);
            rectChartPanel.offsetMax -= new Vector2(speed, 0);
            rectSelectionPanel.offsetMin -= new Vector2(speed, 0);
        }
    }

    public void RenderScreenContent(SelectionModel[] selections)
    {
        foreach (Transform child in SelectionView.content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < selections.Length; i++)
        {
            var instance = GameObject.Instantiate(ButtonPrefab.gameObject) as GameObject;

            instance.transform.SetParent(SelectionView.content, false);

            instance.transform.Find("Label").GetComponent<Text>().text = selections[i].getName();

            var button = instance.GetComponent<CustomButton>();
            button.setId(selections[i].getId());

            button.onClick.AddListener(() =>
            {
                SelectionButtonClicked(button);
            });
        }
    }

    private void OnDestroy()
    {
        foreach (Transform button in SelectionView.content)
        {
            button.gameObject.GetComponent<CustomButton>().onClick.RemoveAllListeners();
        }
    }
}
