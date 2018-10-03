using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainState : BaseState {

    [Inject]
    public MainScreen mainScreen { get; private set; }
    [Inject]
    private SignalBus signalBus;
    [Inject]
    private DeviceModel deviceModel;

    private GameObject model;
    private Animator animator;

    public override void Load()
    {
        mainScreen.ButtonUpClicked += ButtonUpClicked;
        mainScreen.ButtonDownClicked += ButtonDownClicked;
        mainScreen.ButtonChartClicked += ButtonChartClicked;
        mainScreen.ButtonCloseClicked += ButtonCloseClicked;
        base.Load();

        //if (deviceModel.id == 2)
        //{
        //    mainScreen.SetTitle("Комбайн МВ-12");
        //    model = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/comb_adv"), new Vector3(0, 0, 25), Quaternion.identity);
        //    animator = model.GetComponentInChildren<Animator>();
        //} else if (deviceModel.id == 1)
        //{
        //    mainScreen.SetTitle("Ролик прокатный");
        //    model = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/rolik"), new Vector3(0, 0, 25), Quaternion.identity);
        //    animator = model.GetComponentInChildren<Animator>();
        //}
        ///Модель сервера
        model = GameObject.Instantiate(deviceModel.Object);
        animator = model.GetComponentInChildren<Animator>();
        //mainScreen.SetTitle(deviceModel.Name);
        mainScreen.Prompt.text = "3-D Модель устройства. Крутить с помощью джойстиков. Приближать пальцами. Детальный разбор - по стрелочкам слева.";
        mainScreen.GetComponent<ModelRotation>().Model = model.transform;
        mainScreen.GetComponent<Zoom>().Model = model.transform;
        mainScreen.Show();
    }
    public override void Unload()
    {
        base.Unload();
        mainScreen.ButtonUpClicked -= ButtonUpClicked;
        mainScreen.ButtonDownClicked -= ButtonDownClicked;
        mainScreen.ButtonChartClicked -= ButtonChartClicked;
        mainScreen.ButtonCloseClicked -= ButtonCloseClicked;
        GameObject.Destroy(model);
        mainScreen.Hide();
    }

    public void ButtonUpClicked()
    {
        ///Todo hardcode. Hardcode comleted;
        var currentState = animator.GetInteger("state");
        mainScreen.ButtonDown.interactable = true;
        if (deviceModel.id == 1)
        {
            if (currentState < 6)
            {
                animator.SetInteger("state", currentState + 1);
                mainScreen.Prompt.text = "Шаг " + (currentState + 1) + ". На этом шаге делается что-то с фигурой. Осуществляется при помощи инструментов: отвертка, ключ на 12, болгарка";
            }
            if (currentState + 1 == 6)
            {
                mainScreen.ButtonUp.interactable = false;
            }
        }
        else if (deviceModel.id == 2)
        {
            if (currentState < 4)
            {
                animator.SetInteger("state", currentState + 1);
                mainScreen.Prompt.text = "Шаг " + (currentState + 1) + ". На этом шаге делается что-то с фигурой. Осуществляется при помощи инструментов: отвертка, ключ на 12, болгарка";
            }
            if (currentState + 1 == 4)
            {
                mainScreen.ButtonUp.interactable = false;
            }
        }
    }

    public void ButtonDownClicked()
    {
        ///Todo hardcode. Hardcode comleted; 
        var currentState = animator.GetInteger("state");
        mainScreen.ButtonUp.interactable = true;
        if (deviceModel.id == 1)
        {
            if (currentState > 0)
            {
                animator.SetInteger("state", currentState - 1);
                mainScreen.Prompt.text = "Шаг " + (currentState - 1) + ". На этом шаге делается что-то с фигурой. Осуществляется при помощи инструментов: отвертка, ключ на 12, болгарка";
            }
            if (currentState - 1 == 0)
            {
                mainScreen.ButtonDown.interactable = false;
            }
        }
        else if (deviceModel.id == 2)
        {
            if (currentState > 0)
            {
                animator.SetInteger("state", currentState - 1);
                mainScreen.Prompt.text = "Шаг " + (currentState - 1) + ". На этом шаге делается что-то с фигурой. Осуществляется при помощи инструментов: отвертка, ключ на 12, болгарка";
            }
            if (currentState - 1 == 0)
            {
                mainScreen.ButtonDown.interactable = false;
            }
        }
    }

    public void ButtonChartClicked()
    {
        mainScreen.ChartPanel.SetActive(true);
        model.SetActive(false);
    }

    public void ButtonCloseClicked()
    {
        mainScreen.ChartPanel.SetActive(false);
        model.SetActive(true);
    }
}
