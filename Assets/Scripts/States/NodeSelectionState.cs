using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class NodeSelectionState : BaseState {

    public string Id;
    public string Name;

    [Inject]
    public NodeSelectionScreen nodeSelectionScreen;
    [Inject]
    private SignalBus signalBus;

    private NodeModel[] dataModel;

    NodeModel[] GetNodes()
    {
        NodeModel[] tmpNode = new NodeModel[10];
        for (int i = 0; i < 10; i++)
        {
            tmpNode[i] = new NodeModel("" + i, "Node" + Id + "_" + i);
        }
        return tmpNode;
    }

    public override void Load()
    {
        base.Load();
        dataModel = GetNodes();
        nodeSelectionScreen.SetTitle(Name);
        nodeSelectionScreen.NodeButtonClicked += OnNodeButtonClicked;
        nodeSelectionScreen.RenderScreenContent(dataModel);

        nodeSelectionScreen.Show();
    }

    public override void Unload()
    {
        base.Unload();
        nodeSelectionScreen.Hide();
    }

    private void OnNodeButtonClicked(CustomButton button)
    {
        signalBus.Fire(new AssemblingSceneOpenSignal(button.getId(), button.getName()));
    }
}
