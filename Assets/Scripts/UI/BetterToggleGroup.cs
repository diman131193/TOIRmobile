using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class BetterToggleGroup : ToggleGroup
{
    public event Action<Toggle> OnChange = delegate { };

    public void Start()
    {
        foreach (Transform transformToggle in gameObject.transform)
        {
            var toggle = transformToggle.gameObject.GetComponent<Toggle>();
            
            toggle.onValueChanged.AddListener((isSelected) => {
                if (!isSelected)
                {
                    return;
                }
                var activeToggle = Active();
                DoOnChange(activeToggle);
            });
        }
    }
    public Toggle Active()
    {
        return ActiveToggles().FirstOrDefault();
    }

    protected virtual void DoOnChange(Toggle newactive)
    {
        var handler = OnChange;
        if (handler != null) handler(newactive);
    }
}
