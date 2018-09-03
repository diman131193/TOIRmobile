using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class Screen2State : BaseState {

    [Inject]
    public Screen2Screen screen2Screen { get; private set; }
    [Inject]
    private SignalBus signalBus;

    private Dictionary<int, int> instance_model = new Dictionary<int, int>();

    private DepartmentModel[] data_model;

    DepartmentModel[] GetDepartments(int dep_count, int sec_count)
    {
        var results = new DepartmentModel[dep_count];
        var sections = new SectionModel[sec_count];
        for (int i = 0; i < dep_count; i++)
        {
            for (var j = 0; j < sec_count; j++)
            {
                sections[i] = new SectionModel();
                sections[i].Id = int.Parse((i + 1).ToString() + (j + 1).ToString());
                sections[i].Name = "Dep" + (i + 1) + "_Section" + (j + 1);  
            }

            results[i] = new DepartmentModel();
            results[i].Id = i + 1;
            results[i].Name = "Department" + (i + 1);
            results[i].Sections = sections;
        }
        return results;
    }

    public override void Load()
    {
        base.Load();

        int departments_count = 9;
        int sections_count = 9;

        screen2Screen.TglGroup_Change += TglGroup_OnChange;
        data_model = GetDepartments(departments_count, sections_count);
        screen2Screen.RenderScreenContent(data_model, ref instance_model);
        screen2Screen.Show();

    }
    public override void Unload()
    {
        base.Unload();
        screen2Screen.Hide();
    }

    public void TglGroup_OnChange(Toggle newActive)
    { 
        screen2Screen.RenderSections(data_model, instance_model[newActive.gameObject.GetInstanceID()]);
    }

}
