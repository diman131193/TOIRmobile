using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelectionScreen : BaseScreen
{
    [SerializeField]
    public RectTransform ButtonPrefab;

    [SerializeField]
    public ScrollRect ShopView;

    public event Action<CustomButton> ShopButtonClicked = delegate { };

    public void RenderScreenContent(ShopModel[] shops)
    {
        for (int i = 0; i < shops.Length; i++)
        {
            var instance = GameObject.Instantiate(ButtonPrefab.gameObject) as GameObject;

            instance.transform.SetParent(ShopView.content, false);

            instance.transform.Find("Label").GetComponent<Text>().text = shops[i].getName();

            var button = instance.GetComponent<CustomButton>();
            button.setId(shops[i].getId());

            button.onClick.AddListener(() =>
            {
                ShopButtonClicked(button);
            });
        }  
    }

    private void OnDestroy()
    {
        foreach (Transform button in ShopView.content)
        {
            button.gameObject.GetComponent<CustomButton>().onClick.RemoveAllListeners();
        }
    }
}
