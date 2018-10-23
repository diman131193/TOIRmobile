using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button {

    private string Id;

    public void setId(string id)
    {
        this.Id = id;
    }

    public string getId()
    {
        return this.Id;
    }

    public string getName()
    {
        return this.transform.Find("Label").GetComponent<Text>().text;
    }
}
