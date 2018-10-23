using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeSelectionScreen : BaseScreen
{
    [SerializeField]
    public RectTransform ButtonPrefab;

    [SerializeField]
    public ScrollRect NodeView;

    public event Action<CustomButton> NodeButtonClicked = delegate { };

    public void RenderScreenContent(NodeModel[] nodes)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            var instance = GameObject.Instantiate(ButtonPrefab.gameObject) as GameObject;

            instance.transform.SetParent(NodeView.content, false);

            instance.transform.Find("Label").GetComponent<Text>().text = nodes[i].getName();

            var button = instance.GetComponent<CustomButton>();
            button.setId(nodes[i].getId());

            button.onClick.AddListener(() =>
            {
                NodeButtonClicked(button);
            });
        }
    }

    private void OnDestroy()
    {
        foreach (Transform button in NodeView.content)
        {
            button.gameObject.GetComponent<CustomButton>().onClick.RemoveAllListeners();
        }
    }
}
