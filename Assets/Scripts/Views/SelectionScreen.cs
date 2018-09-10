using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectionScreen : BaseScreen 
{
    [SerializeField]
    public ScrollRect ViewDepartment;

    [SerializeField]
    public ScrollRect ViewSection;

    [SerializeField]
    public ScrollRect ViewObject;

    [SerializeField]
    public RectTransform TogglePrefab;

    [SerializeField]
    public BetterToggleGroup department_group;
    [SerializeField]
    public BetterToggleGroup section_group;
    [SerializeField]
    public BetterToggleGroup object_group;

    public event Action<Toggle> DepartmentTglGroup_Change = delegate { };
    public event Action<Toggle> SectionTglGroup_Change = delegate { };
    public event Action<Toggle> ObjectTglGroup_Change = delegate { };

    void Start()
    {       
        department_group.OnChange += DepartmentTglGroup_Change;
        section_group.OnChange += SectionTglGroup_Change;
        object_group.OnChange += ObjectTglGroup_Change;

    }



    public void RenderScreenContent(DepartmentModel[] departments)
    {
        Debug.Log(departments[0].Name);

        RenderDepartments(departments);
        
    }

    public void RenderDepartments(DepartmentModel[] departments)
    {

        //foreach (Transform child in ViewDepartment.content)
        //{
        //    Destroy(child.gameObject);
        //}
        if (ViewDepartment.content.childCount == 0)
        {
            for (var i = 0; i < departments.Length; i++)
            {
                var instance = GameObject.Instantiate(TogglePrefab.gameObject) as GameObject;
                instance.transform.SetParent(ViewDepartment.content, false);
                instance.transform.Find("Label").GetComponent<Text>().text = departments[i].Name;
                var toggle = instance.GetComponent<CustomToggle>();
                toggle.group = ViewDepartment.content.GetComponent<BetterToggleGroup>();
                toggle.Id = i;
            }
        }

        department_group.Start();
    }

    public void RenderSections(SectionModel[] sections)
    {
        foreach (Transform child in ViewSection.content)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ViewObject.content)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < sections.Length; i++)
        {
            var instance = GameObject.Instantiate(TogglePrefab.gameObject) as GameObject;
            instance.transform.SetParent(ViewSection.content, false);
            instance.transform.Find("Label").GetComponent<Text>().text = sections[i].Name;
            var toggle = instance.GetComponent<CustomToggle>();
            toggle.group = ViewSection.content.GetComponent<BetterToggleGroup>();
            toggle.Id = i;
        }

        section_group.Start();

    }

    public void RenderObjects(DeviceModel[] objects)
    {
        foreach (Transform child in ViewObject.content)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < objects.Length; i++)
        {
            var instance = GameObject.Instantiate(TogglePrefab.gameObject) as GameObject;
            instance.transform.SetParent(ViewObject.content, false);
            instance.transform.Find("Label").GetComponent<Text>().text = objects[i].Name;
            var toggle = instance.GetComponent<CustomToggle>();
            toggle.group = ViewObject.content.GetComponent<BetterToggleGroup>();
            /// Костыль для сервера
            toggle.Id = i+1;
        }

        object_group.Start();

    }

    private void OnDestroy()
    {
        department_group.OnChange -= DepartmentTglGroup_Change;
        section_group.OnChange -= SectionTglGroup_Change;
        object_group.OnChange -= ObjectTglGroup_Change;
    }
}
