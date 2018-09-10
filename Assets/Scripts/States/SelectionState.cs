using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;

public class SelectionState : BaseState {

    [Inject]
    public SelectionScreen selectionScreen { get; private set; }
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
                sections[j].Name = "Участок " + (j + 1);
                sections[j].Objects = new DeviceModel[] { new DeviceModel() { Name = "Мешалка", id = 1 }, new DeviceModel() { Name = "Трактор", id = 2 } };
            }

            results[i] = new DepartmentModel();
            results[i].Id = i;
            results[i].Name = "Цех " + (i + 1);
            results[i].Sections = sections;
        }
        return results;
    }

    public override void Load()
    {
        base.Load();

        int departments_count = 9;
        int sections_count = 9;

        selectionScreen.DepartmentTglGroup_Change += TglGroup_OnChange;
        selectionScreen.SectionTglGroup_Change += SectionTglGroup_Change;
        selectionScreen.ObjectTglGroup_Change += ObjectTglGroup_Change;

        data_model = GetDepartments(departments_count, sections_count);

        selectionScreen.RenderScreenContent(data_model);
        selectionScreen.SetTitle("Выбор узла");
        selectionScreen.Show();

    }


    public override void Unload()
    {
        base.Unload();
        selectionScreen.DepartmentTglGroup_Change -= TglGroup_OnChange;
        selectionScreen.SectionTglGroup_Change -= SectionTglGroup_Change;
        selectionScreen.ObjectTglGroup_Change -= ObjectTglGroup_Change;
        selectionScreen.Hide();
    }

    public void TglGroup_OnChange(Toggle newActive)
    {
        currDepartment = data_model.Where(x => x.Id == ((CustomToggle)newActive).Id).FirstOrDefault();
        selectionScreen.RenderSections(currDepartment.Sections);
    }


    private void ObjectTglGroup_Change(Toggle obj)
    {
        signalBus.Fire(new LoadSceneOpenSignal() { id = ((CustomToggle)obj).Id });
    }

    private void SectionTglGroup_Change(Toggle obj)
    {
        currSection = currDepartment.Sections.Where(x => x.Id == ((CustomToggle)obj).Id).FirstOrDefault();
        selectionScreen.RenderObjects(currSection.Objects);
    }
}
