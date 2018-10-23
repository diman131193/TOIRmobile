using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MainSceneOpenSignal {
    private AssetBundle Bundle;
    private int Id;
    private string Name;

    public MainSceneOpenSignal(AssetBundle bundle, int id, string name)
    {
        this.Bundle = bundle;
        this.Id = id;
        this.Name = name;
    }

    public AssetBundle getBundle()
    {
        return this.Bundle;
    }

    public int getId()
    {
        return this.Id;
    }

    public string getName()
    {
        return this.Name;
    }
}
