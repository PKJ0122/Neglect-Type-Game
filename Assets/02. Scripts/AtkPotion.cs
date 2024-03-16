using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtkPotion : MonoBehaviour
{
    public Button potionEatButton;

    public Image potionCoolTime;
    public Text remainingTimeText;
    public Text potionRemainingAmountText;

    bool potionTime;

    public static Action potionRemainingAmountTextSet;

    void Start()
    {
        potionEatButton.onClick.AddListener(EatPotion);
        potionRemainingAmountTextSet += SetPotionRemainingAmount;
        potionRemainingAmountTextSet.Invoke();

        if (GameManager.instance.playerData.potionRemainingTime != 0)
        {
            potionCoolTime.gameObject.SetActive(true);
            remainingTimeText.gameObject.SetActive(true);
            potionTime = true;
        }
    }

    void Update()
    {
        if (potionTime)
        {
            GameManager.instance.playerData.potionRemainingTime -= Time.deltaTime;
            int time = (int)(GameManager.instance.playerData.potionRemainingTime / 60);
            remainingTimeText.text = $"{time}분";
            float cooltime = GameManager.instance.playerData.potionRemainingTime / 7200;
            potionCoolTime.fillAmount = cooltime;

            if(GameManager.instance.playerData.potionRemainingTime <= 0)
                EndPotion();
        }
    }

    void EatPotion()
    {
        if (GameManager.instance.playerData.potionQuantity == 0)
            return;

        potionTime = true;
        PlayerInfo.atkPotion.Invoke(true);
        potionCoolTime.gameObject.SetActive(true);
        remainingTimeText.gameObject.SetActive(true);
        GameManager.instance.playerData.potionQuantity--;
        GameManager.instance.playerData.potionRemainingTime += 7200;
        potionRemainingAmountTextSet.Invoke();
    }

    void EndPotion()
    {
        potionTime = false;
        PlayerInfo.atkPotion.Invoke(false);
        GameManager.instance.playerData.potionRemainingTime = 0;
        potionCoolTime.gameObject.SetActive(false);
        remainingTimeText.gameObject.SetActive(false);
    }

    void SetPotionRemainingAmount()
    {
        potionRemainingAmountText.text = $"{GameManager.instance.playerData.potionQuantity}개 보유중";
    }
}