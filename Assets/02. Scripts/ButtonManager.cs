using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button encyclopediaButton;
    public Button inventoryButton;
    public Button shopButton;
    public Button settingButton;

    public GameObject[] popupObject;

    void Start()
    {
        encyclopediaButton.onClick.AddListener(()=> PopUpOnOff(0));
        inventoryButton.onClick.AddListener(() => PopUpOnOff(1));
        shopButton.onClick.AddListener(() => PopUpOnOff(2));
        settingButton.onClick.AddListener(() => PopUpOnOff(3));
    }

    void PopUpOnOff(int ButtonIndex)
    {
        popupObject[ButtonIndex].SetActive(true);
    }
}
