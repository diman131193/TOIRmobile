using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseClick: MonoBehaviour {
    public Model model;
    bool singleTouch;
    float doubleClickSpeed = 1.5f;
    float timeElapsed = 0.0f;

    void OnMouseDown()
    {
        if (!singleTouch)
        {
                print(" Single Tap");
                singleTouch = true;
                if (GetInstanceID() != model.SelectedId)
                {
                    foreach (Renderer child in model.GetComponentsInChildren<Renderer>())
                    {
                        child.material.color = Color.white;
                    }
                    GetComponent<Renderer>().material.color = Color.blue;
                    model.SelectedId = GetInstanceID();
                }
        }
        else
        {
            print("Double Tap");
            if (GetInstanceID() == model.SelectedId && GetComponent<Animator>())
            {
                GetComponent<Animator>().enabled = true;
            }
            
            singleTouch = false;
            timeElapsed = 0f;
        }
    }

    private void Update()
    {
        if (singleTouch)
        {
            timeElapsed += Time.deltaTime;
        }
        if (timeElapsed > doubleClickSpeed)
        {
            print("Values Resetted");
            timeElapsed = 0f;
            singleTouch = false;
        }
    }
}
