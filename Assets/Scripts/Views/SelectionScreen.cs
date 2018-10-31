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

    public event Action<CustomButton> SelectionButtonClicked = delegate { };
    public event Action _3DButtonClicked = delegate { };

    void Start()
    {
        _3D.onClick.AddListener(() =>
        {
            _3DButtonClicked();
        });
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
