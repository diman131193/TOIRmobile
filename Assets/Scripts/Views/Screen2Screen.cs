using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Screen2Screen : BaseScreen 
{
    [SerializeField]
    public ScrollRect ViewDepartment;

    [SerializeField]
    public ScrollRect ViewSection;

    [SerializeField]
    public RectTransform TogglePrefab;

    [SerializeField]
    public BetterToggleGroup department_group;

    public event Action<Toggle> TglGroup_Change = delegate { };

    void Start()
    {       
        department_group.OnChange += TglGroup_Change;
    }


    public void RenderScreenContent(DepartmentModel[] departments, ref Dictionary<int, int> result)
    {
        Debug.Log(departments[0].Name);
        foreach (Transform child in ViewSection.content)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ViewDepartment.content)
        {
            Destroy(child.gameObject);
        }

        RenderDepartments(departments, ref result);
        department_group.Start();
    }

    public void RenderDepartments(DepartmentModel[] departments, ref Dictionary<int, int> result)
    {
        foreach (var department in departments)
        {           
            var instance = GameObject.Instantiate(TogglePrefab.gameObject) as GameObject;
            instance.transform.SetParent(ViewDepartment.content, false);
            instance.transform.Find("Label").GetComponent<Text>().text = department.Name;
            result.Add(instance.GetInstanceID(), department.Id);

            instance.GetComponent<Toggle>().group = ViewDepartment.content.GetComponent<BetterToggleGroup>();
            
        }
    }

    public void RenderSections(DepartmentModel[] data_model, int id)
    {
        foreach (var dep in data_model)
        {
            if (id == dep.Id)
            {
                foreach(var sec in dep.Sections)
                {
                    var instance = GameObject.Instantiate(TogglePrefab.gameObject) as GameObject;
                    instance.transform.SetParent(ViewSection.content, false);
                    instance.transform.Find("Label").GetComponent<Text>().text = sec.Name;
                    instance.GetComponent<Toggle>().group = ViewSection.content.GetComponent<BetterToggleGroup>();
                }

            }
        }
    }

    private void OnDestroy()
    {
    }
}
