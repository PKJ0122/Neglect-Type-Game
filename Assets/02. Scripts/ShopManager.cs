using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public SwordData swordData;

    public ShopData shopData;

    public Text playerAtkLevelText;
    public Text playerAtkLevelUpPriceText;
    public Button playerAtkLevelUpButton;

    public Button swordBuyButton;
    public GameObject swordBuyResultPanel;
    public Image swordBuyImage;
    public Text swordBuyAmountText;

    void Start()
    {
        playerAtkLevelUpButton.onClick.AddListener(PlayerLevelUp);
        swordBuyButton.onClick.AddListener(SwordBuyButton);
        SetShopPanel();
    }

    void SetShopPanel()
    {
        playerAtkLevelText.text = $"°ø°Ý·Â LV.{GameManager.instance.playerData.playerAtkLevel}";
        playerAtkLevelUpPriceText.text =
        $"{string.Format("{0:#,###}", shopData.playerAtkLevelUpPrice[GameManager.instance.playerData.playerAtkLevel])}";
    }

    void PlayerLevelUp()
    {
        if (shopData.playerAtkLevelUpPrice[GameManager.instance.playerData.playerAtkLevel] >
            GameManager.instance.playerData.gold)
        {
            return;
        }

        GameManager.instance.playerData.gold -= shopData.playerAtkLevelUpPrice[GameManager.instance.playerData.playerAtkLevel];
        GameManager.instance.playerData.playerAtkLevel++;
        SetShopPanel();
        PlayerInfo.playerSetInfo.Invoke();
    }

    void SwordBuyButton()
    {
        if (GameManager.instance.playerData.gold < 1000 || !GameManager.instance.inventoryData.inventorys.Contains(-1))
        {
            return;
        }

        int result = RandomSword();
        int inventoryNumber = Array.IndexOf(GameManager.instance.inventoryData.inventorys, -1);
        GameManager.instance.playerData.gold -= 1000;
        GameManager.instance.inventoryData.inventorys[inventoryNumber] = result;
        swordBuyImage.sprite = swordData.swordDatas[result].swordSprite;
        swordBuyAmountText.text = $"+{result}";
        swordBuyResultPanel.SetActive(true);
        InventoryManager.inventorySet.Invoke();
        PlayerInfo.playerSetInfo.Invoke();
    }

    int RandomSword()
    {
        int RandomNumber = UnityEngine.Random.Range(1, 1001);

        if(RandomNumber <= 1)
            return 7;
        else if(RandomNumber <= 3)
            return 6;
        else if (RandomNumber <= 6)
            return 5;
        else if (RandomNumber <= 10)
            return 4;
        else if (RandomNumber <= 60)
            return 3;
        else if (RandomNumber <= 160)
            return 2;
        else if (RandomNumber <= 500)
            return 1;
        else
            return 0;
    }
}