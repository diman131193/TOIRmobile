using System;
using UnityEngine;
using UnityEngine.UI;

public class SectorSelectionScreen : BaseScreen
{
    [SerializeField]
    public RectTransform ButtonPrefab;

    [SerializeField]
    public ScrollRect SectorView;

    public event Action<CustomButton> SectorButtonClicked = delegate { };

    public void RenderScreenContent(SectorModel[] sectors)
    {
        foreach (Transform child in SectorView.content)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < sectors.Length; i++)
        {
            var instance = GameObject.Instantiate(ButtonPrefab.gameObject) as GameObject;

            instance.transform.SetParent(SectorView.content, false);

            instance.transform.Find("Label").GetComponent<Text>().text = sectors[i].getName();

            var button = instance.GetComponent<CustomButton>();
            button.setId(sectors[i].getId());

            button.onClick.AddListener(() =>
            {
                SectorButtonClicked(button);
            });
        }
    }

    private void OnDestroy()
    {
        foreach (Transform button in SectorView.content)
        {
            button.gameObject.GetComponent<CustomButton>().onClick.RemoveAllListeners();
        }
    }
}
