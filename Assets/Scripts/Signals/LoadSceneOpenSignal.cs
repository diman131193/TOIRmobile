using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LoadSceneOpenSignal {

    private int Id;
    private string Name;

    public LoadSceneOpenSignal(int id, string name)
    {
        this.Id = id;
        this.Name = name;
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

