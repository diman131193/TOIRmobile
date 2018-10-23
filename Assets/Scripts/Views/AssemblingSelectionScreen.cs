using System;
using UnityEngine;
using UnityEngine.UI;

public class AssemblingSelectionScreen : BaseScreen
{
    [SerializeField]
    public RectTransform ButtonPrefab;

    [SerializeField]
    public ScrollRect AssemblingView;

    public event Action<CustomButton> AssemblingButtonClicked = delegate { };

    public void RenderScreenContent(AssemblingModel[] assemblings)
    {
        foreach (Transform child in AssemblingView.content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < assemblings.Length; i++)
        {
            var instance = GameObject.Instantiate(ButtonPrefab.gameObject) as GameObject;

            instance.transform.SetParent(AssemblingView.content, false);

            instance.transform.Find("Label").GetComponent<Text>().text = assemblings[i].getName();

            var button = instance.GetComponent<CustomButton>();
            button.setId(assemblings[i].getId());

            button.onClick.AddListener(() =>
            {
                AssemblingButtonClicked(button);
            });
        }
    }

    private void OnDestroy()
    {
        foreach (Transform button in AssemblingView.content)
        {
            button.gameObject.GetComponent<CustomButton>().onClick.RemoveAllListeners();
        }
    }
}
