using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private float colorIntensity = 185.0f;

    void Update()
    {
        this.gameObject.GetComponent<Image>().color = new Color(colorIntensity / 255.0f, colorIntensity / 255.0f, colorIntensity / 255.0f, 255.0f / 255.0f);
    }

    public void changeColor(float value)
    {
        colorIntensity = 255.0f - value;
    }
}
