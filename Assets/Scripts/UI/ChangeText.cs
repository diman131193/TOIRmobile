using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    private float colorIntensity = 70.0f;
    private float colorFix = 200.0f;

    void Update()
    {
        this.gameObject.GetComponent<Text>().color = new Color(colorIntensity / 255.0f, colorIntensity / 255.0f, colorFix / 255.0f, 255.0f / 255.0f);
    }

    public void changeText(float value)
    {
        colorIntensity = value;
    }
}
