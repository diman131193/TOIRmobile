using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class Screen2State : BaseState {

    [Inject]
    public Screen2Screen screen2Screen { get; private set; }
    [Inject]
    private SignalBus signalBus;
    
    private DepartmentModel[] data_model;
    private SectionModel currSection;
    private DepartmentModel currDepartment;

    DepartmentModel[] GetDepartments(int dep_count, int sec_count)
    {
        var results = new DepartmentModel[dep_count];
        
        for (int i = 0; i < dep_count; i++)
        {
            var sections = new SectionModel[sec_count];
            for (var j = 0; j < sec_count; j++)
            {
                sections[j] = new SectionModel();
                sections[j].Id = j;
                sections[j].Name = "Dep" + (i + 1) + "_Section" + (j + 1);
                sections[j].Objects = new DeviceModel[] { new DeviceModel() { Name = "Model1", id = 1 }, new DeviceModel() { Name = "Model2", id = 2 } };
            }

            results[i] = new DepartmentModel();
            results[i].Id = i;
            results[i].Name = "Department" + (i);
            results[i].Sections = sections;
        }
        return results;
    }

    public override void Load()
    {
        base.Load();

        int departments_count = 9;
        int sections_count = 9;

        screen2Screen.DepartmentTglGroup_Change += TglGroup_OnChange;
        screen2Screen.SectionTglGroup_Change += SectionTglGroup_Change;
        screen2Screen.ObjectTglGroup_Change += ObjectTglGroup_Change;

        data_model = GetDepartments(departments_count, sections_count);

        screen2Screen.RenderScreenContent(data_model);
        screen2Screen.Show();

    }


    public override void Unload()
    {
        base.Unload();
        screen2Screen.DepartmentTglGroup_Change -= TglGroup_OnChange;
        screen2Screen.SectionTglGroup_Change -= SectionTglGroup_Change;
        screen2Screen.ObjectTglGroup_Change -= ObjectTglGroup_Change;
        screen2Screen.Hide();
    }

    public void TglGroup_OnChange(Toggle newActive)
    {
        currDepartment = data_model.Where(x => x.Id == ((CustomToggle)newActive).Id).FirstOrDefault();
        screen2Screen.RenderSections(currDepartment.Sections);
    }


    private void ObjectTglGroup_Change(Toggle obj)
    {
        signalBus.Fire(new LoadSceneOpenSignal() { id = ((CustomToggle)obj).Id });
    }

    private void SectionTglGroup_Change(Toggle obj)
    {
        currSection = currDepartment.Sections.Where(x => x.Id == ((CustomToggle)obj).Id).FirstOrDefault();
        screen2Screen.RenderObjects(currSection.Objects);
    }
}
