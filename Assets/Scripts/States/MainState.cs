using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainState : BaseState {

    [Inject]
    public MainScreen mainScreen { get; private set; }
    [Inject]
    private SignalBus signalBus;
    
    public AssetBundle Bundle { get; set; }
    public int Id;
    public string Name;

    private GameObject model;
    private Animator animator;
    
    private DeviceModel deviceModel = new DeviceModel();

    public override void Load()
    {
        mainScreen.ButtonUpClicked += ButtonUpClicked;
        mainScreen.ButtonDownClicked += ButtonDownClicked;
        mainScreen.ButtonChartClicked += ButtonChartClicked;
        mainScreen.ButtonCloseClicked += ButtonCloseClicked;
        base.Load();
        Instantiate();
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
        Bundle.Unload(true);
        mainScreen.Hide();
    }

    public void ButtonUpClicked()
    {
        ///Todo hardcode. Hardcode comleted;
        var currentState = animator.GetInteger("state");
        mainScreen.ButtonDown.interactable = true;
        if (deviceModel.id == 1)
        {
            
            if (currentState < 2)
            {
                animator.SetInteger("state", currentState + 1);
                mainScreen.Prompt.text = "Шаг " + (currentState + 1) + ". Открутить болты и снять наружнюю крышку с подшипниковой опоры.\nИнструменты: Ключ гаечный №18, зубило, молоток.";
            }
            if (currentState + 1 == 2)
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
            if (currentState > 1)
            {
                animator.SetInteger("state", currentState - 1);
                mainScreen.Prompt.text = "Шаг " + (currentState - 1) + ". Открутить болты и снять наружнюю крышку с подшипниковой опоры.\nИнструменты: Ключ гаечный №18, зубило, молоток.";
            }
            if (currentState == 1)
            {
                animator.SetInteger("state", currentState - 1);
                mainScreen.Prompt.text = "3-D Модель устройства. Крутить с помощью джойстиков. Приближать пальцами. Детальный разбор - по стрелочкам слева.";
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

    private void Instantiate()
    {
        var prefab = Bundle.LoadAsset<GameObject>("rolik.prefab");
        deviceModel.Object = prefab;
        deviceModel.id = Id;
        mainScreen.SetTitle(Name);
        model = GameObject.Instantiate(deviceModel.Object);
        model.layer = 13;
        foreach(Transform child in model.GetComponentInChildren<Transform>())
        {
            child.gameObject.layer = 13;
        }
        animator = model.GetComponentInChildren<Animator>();
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
