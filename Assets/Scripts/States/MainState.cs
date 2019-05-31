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
    public string Id;
    public string Name;

    private GameObject model;
    private Animator animator;

    private AnimatorClipInfo[] animatorClipInfo;

    private DeviceModel deviceModel = new DeviceModel();

    private InstructionModel[] instruction;

    public override void Load()
    {
        
        base.Load();
        if (Bundle)
        {
            mainScreen.mainPanel.SetActive(true);
            mainScreen.ButtonUpClicked += ButtonUpClicked;
            mainScreen.ButtonDownClicked += ButtonDownClicked;
            mainScreen.ButtonSettingsClicked += ButtonSettingsClicked;
            mainScreen.ButtonInstrumentClicked += ButtonInstrumentClicked;
            mainScreen.ButtonClockClicked += ButtonClockClicked;
            mainScreen.ButtonDown.interactable = false;
            mainScreen.ButtonUp.interactable = true;
            mainScreen.OperationType.text = "Начало работы.";
            mainScreen.ButtonSettings.interactable = false;
            mainScreen.ButtonInstrument.interactable = false;
            mainScreen.ButtonClock.interactable = false;
            Instantiate();
            mainScreen.Description.text = "<b>Крутить</b> с помощью джойстиков. <b>Приближать</b> двумя пальцами. <b>Детальный разбор</b> по стрелочкам слева. Для получения подробной инструкции нажмите слева соответственно на: <b>Описание</b>, <b>Инструмены</b> ,<b>Время</b>.";
            mainScreen.GetComponent<ModelRotation>().Model = model.transform;
            mainScreen.GetComponent<Zoom>().Model = model.transform;
            mainScreen.GetComponent<Zoom>().zMin = model.transform.position.z - 5.0f;
            mainScreen.GetComponent<Zoom>().zMax = model.transform.position.z + 10.0f;
            mainScreen.GetComponent<ModelPosition>().Model = model.transform;
            mainScreen.Show();
            mainScreen.StartCoroutine(Helpers.EntityHelper.getInstructions(Id, value => GetInstruction(value)));
        }
        mainScreen.SetTitle(Name);
    }

    public override void Unload()
    {
        base.Unload();
        if (Bundle)
        {
            mainScreen.ButtonUpClicked -= ButtonUpClicked;
            mainScreen.ButtonDownClicked -= ButtonDownClicked;
            mainScreen.ButtonSettingsClicked -= ButtonSettingsClicked;
            mainScreen.ButtonInstrumentClicked -= ButtonInstrumentClicked;
            mainScreen.ButtonClockClicked -= ButtonClockClicked;
            GameObject.Destroy(model);
            Bundle.Unload(true);
            mainScreen.mainPanel.SetActive(false);
            mainScreen.Hide();
        }
        

        
    }

    void GetInstruction(InstructionModel[] res)
    {
        instruction = res;
    }

    public void ButtonUpClicked()
    {
        var currentState = animator.GetInteger("state");
        currentState++;
        mainScreen.ButtonDown.interactable = true;
        

        string description = "";
   
        if (currentState <= instruction.Length)
        {
            animator.SetInteger("state", currentState);
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "D") description = "Демонтаж";
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "R") description = "Разборка";
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "S") description = "Сборка";
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "M") description = "Монтаж";
            mainScreen.OperationType.text = "" + currentState + ". " + description;
            mainScreen.ButtonSettings.interactable = false;
            mainScreen.ButtonInstrument.interactable = true;
            mainScreen.ButtonClock.interactable = true;
            mainScreen.Description.text = "<b><size=30>Описание:</size></b>\n" + instruction[currentState - 1].LONG_DESCRIPTION;
        }
        mainScreen.ButtonUp.interactable = false;
        //animatorClipInfo = animator.GetCurrentAnimatorClipInfo(currentState);
        //Debug.Log(animatorClipInfo[0].clip.name);
        mainScreen.StartCoroutine(WaitForAnim(currentState));
    }

    public void ButtonDownClicked()
    {
        ///Todo hardcode. Hardcode comleted; 
        var currentState = animator.GetInteger("state");
        mainScreen.ButtonUp.interactable = true;
        currentState--;
        string description = "";

        if (currentState > 0)
        {
            animator.SetInteger("state", currentState);
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "D") description = "Демонтаж";
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "R") description = "Разборка";
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "S") description = "Сборка";
            if (instruction[currentState - 1].OPERATION_DESCRIPTION == "M") description = "Монтаж";
            mainScreen.OperationType.text = "" + currentState + ". " + description;
            mainScreen.ButtonSettings.interactable = false;
            mainScreen.ButtonInstrument.interactable = true;
            mainScreen.ButtonClock.interactable = true;
            mainScreen.Description.text = "<b><size=30>Описание:</size></b>\n" + instruction[currentState - 1].LONG_DESCRIPTION;
        }
        if (currentState == 0)
        {
            animator.SetInteger("state", currentState);
            mainScreen.OperationType.text = "Начало работы.";
            mainScreen.ButtonSettings.interactable = false;
            mainScreen.ButtonInstrument.interactable = false;
            mainScreen.ButtonClock.interactable = false;
            mainScreen.Description.text = "<b>Крутить</b> с помощью джойстиков. <b>Приближать</b> двумя пальцами. <b>Детальный разбор</b> по стрелочкам слева. Для получения подробной инструкции нажмите слева соответственно на: <b>Описание</b>, <b>Инструмены</b> ,<b>Время</b>.";
            mainScreen.ButtonDown.interactable = false;
        }
    }

    public void ButtonSettingsClicked()
    {
        var currentState = animator.GetInteger("state");
        mainScreen.ButtonSettings.interactable = false;
        mainScreen.ButtonInstrument.interactable = true;
        mainScreen.ButtonClock.interactable = true;
        mainScreen.Description.text = "<b><size=30>Описание:</size></b>\n" + instruction[currentState - 1].LONG_DESCRIPTION;
    }

    public void ButtonInstrumentClicked()
    {
        var currentState = animator.GetInteger("state");
        mainScreen.ButtonSettings.interactable = true;
        mainScreen.ButtonInstrument.interactable = false;
        mainScreen.ButtonClock.interactable = true;
        mainScreen.Description.text = "<b><size=30>Инструменты:</size></b>\n" + instruction[currentState - 1].INSTRUMENTS;
    }

    public void ButtonClockClicked()
    {
        var currentState = animator.GetInteger("state");
        mainScreen.ButtonSettings.interactable = true;
        mainScreen.ButtonInstrument.interactable = true;
        mainScreen.ButtonClock.interactable = false;
        mainScreen.Description.text = "<b><size=30>Время:</size></b>\n" + instruction[currentState - 1].OPERATION_TIME;
    }

    private void Instantiate()
    {
        var prefab = Bundle.LoadAsset<GameObject>(Id + ".prefab");
        deviceModel.Object = prefab;
        deviceModel.id = Id;
        model = GameObject.Instantiate(deviceModel.Object);
        model.layer = 13;
        foreach(Transform child in model.GetComponentInChildren<Transform>())
        {
            child.gameObject.layer = 13;
        }
        animator = model.GetComponentInChildren<Animator>();
    }

    IEnumerator WaitForAnim(int currstate)
    {
        Debug.Log(currstate);

        yield return new WaitForSeconds(1);
        while (animator.GetCurrentAnimatorStateInfo(currstate).IsName(currstate.ToString()) && animator.GetCurrentAnimatorStateInfo(currstate).normalizedTime < 1.0f)
        {
            Debug.Log("Wait...");
            yield return null;
        }
        Debug.Log("Done Playing");
        if (currstate == instruction.Length) mainScreen.ButtonUp.interactable = false;
        else mainScreen.ButtonUp.interactable = true;
    }

}
