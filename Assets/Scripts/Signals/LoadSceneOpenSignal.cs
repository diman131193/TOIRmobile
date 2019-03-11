using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LoadSceneOpenSignal {

    private string Id;
    private string Name;

    public LoadSceneOpenSignal(string id, string name)
    {
        this.Id = id;
        this.Name = name;
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

