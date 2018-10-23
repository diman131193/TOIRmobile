using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionScreen : BaseScreen
{
    [SerializeField]
    public RectTransform ButtonPrefab;

    [SerializeField]
    public ScrollRect UnitView;

    public event Action<CustomButton> UnitButtonClicked = delegate { };

    public void RenderScreenContent(UnitModel[] units)
    {
        for (int i = 0; i < units.Length; i++)
        {
            var instance = GameObject.Instantiate(ButtonPrefab.gameObject) as GameObject;

            instance.transform.SetParent(UnitView.content, false);

            instance.transform.Find("Label").GetComponent<Text>().text = units[i].getName();

            var button = instance.GetComponent<CustomButton>();
            button.setId(units[i].getId());

            button.onClick.AddListener(() =>
            {
                UnitButtonClicked(button);
            });
        }
    }

    private void OnDestroy()
    {
        foreach (Transform button in UnitView.content)
        {
            button.gameObject.GetComponent<CustomButton>().onClick.RemoveAllListeners();
        }
    }
}
