using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LoadSceneOpenSignal {
    private int v;

    public LoadSceneOpenSignal(int v) : this()
    {
        this.v = v;
    }

    public int id { get; set; }
}
