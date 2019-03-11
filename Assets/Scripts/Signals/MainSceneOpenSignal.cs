using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MainSceneOpenSignal {
    private AssetBundle Bundle;
    private string Id;
    private string Name;

    public MainSceneOpenSignal(AssetBundle bundle, string id, string name)
    {
        this.Bundle = bundle;
        this.Id = id;
        this.Name = name;
    }

    public AssetBundle getBundle()
    {
        return this.Bundle;
    }

    public string getId()
    {
        return this.Id;
    }

    public string getName()
    {
        return this.Name;
    }
}
