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

        results[0] = new DepartmentModel();
        results[0].Id = 0;
        results[0].Name = "ККЦ";

        var sections = new SectionModel[sec_count];

        sections[0] = new SectionModel();
        sections[0].Id = 0;
        sections[0].Name = "Участок огнеупорных работ";
        //sections[0].Objects = new DeviceModel[] { new DeviceModel() { Name = "Мешалка", id = 1 }, new DeviceModel() { Name = "Трактор", id = 2 } };

        sections[1] = new SectionModel();
        sections[1].Id = 1;
        sections[1].Name = "Участок выплавки стали. Отделение перелива чугуна";

        sections[2] = new SectionModel();
        sections[2].Id = 2;
        sections[2].Name = "Участок отгрузки готовой продукции";

        results[0].Sections = sections;

        results[1] = new DepartmentModel();
        results[1].Id = 1;
        results[1].Name = "ПЦ";

        var sections1 = new SectionModel[sec_count];

        sections1[0] = new SectionModel();
        sections1[0].Id = 0;
        sections1[0].Name = "Участок стана горячей прокатки 9501";
        //sections[0].Objects = new DeviceModel[] { new DeviceModel() { Name = "Мешалка", id = 1 }, new DeviceModel() { Name = "Трактор", id = 2 } };

        sections1[1] = new SectionModel();
        sections1[1].Id = 1;
        sections1[1].Name = "Участок стана горячей прокатки 9511";
        sections1[1].Objects = new DeviceModel[] { new DeviceModel() { Name = "Ролик прокатный 1", id = 1 } /*, new DeviceModel() { Name = "Комбайн МВ-12", id = 2 } */};

        sections1[2] = new SectionModel();
        sections1[2].Id = 2;
        sections1[2].Name = "Участок термоупрочнения проволочный блок";

        results[1].Sections = sections1;
        //for (int i = 0; i < dep_count; i++)
        //{
        //    var sections = new SectionModel[sec_count];
        //    for (var j = 0; j < sec_count; j++)
        //    {
        //        sections[j] = new SectionModel();
        //        sections[j].Id = j;
        //        sections[j].Name = "Участок " + (j + 1);
        //        sections[j].Objects = new DeviceModel[] { new DeviceModel() { Name = "Мешалка", id = 1 }, new DeviceModel() { Name = "Трактор", id = 2 } };
        //    }

        //    results[i] = new DepartmentModel();
        //    results[i].Id = i;
        //    results[i].Name = "Цех " + (i + 1);
        //    results[i].Sections = sections;
        //}
        return results;
    }

    public override void Load()
    {
        base.Load();

        int departments_count = 2;
        int sections_count = 3;

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
        signalBus.Fire(new LoadSceneOpenSignal(((CustomToggle)obj).Id, "Ролик прокатный 1" ));
    }

    private void SectionTglGroup_Change(Toggle obj)
    {
        currSection = currDepartment.Sections.Where(x => x.Id == ((CustomToggle)obj).Id).FirstOrDefault();
        selectionScreen.RenderObjects(currSection.Objects);
    }
}
