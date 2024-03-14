using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    
    void Start()
    {
        bgmSlider.onValueChanged.AddListener(v => SetSliderValue("Bgm",v));
        sfxSlider.onValueChanged.AddListener(v => SetSliderValue("Sfx",v));

        bgmSlider.value = GameManager.instance.playerData.bgmLevel;
        sfxSlider.value = GameManager.instance.playerData.sfxLevel;
    }

    void SetSliderValue(string discrimination,float Value)
    {
        switch (discrimination)
        {
            case "Bgm":
                GameManager.instance.playerData.bgmLevel = Value;
                break;
            case "Sfx":
                GameManager.instance.playerData.sfxLevel = Value;
                break;
        }
        SoundManager.soundSet.Invoke();
    }
}
