using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ShopData shopData;

    public Text playerAtkLevelText;
    public Text playerAtkSpeedLevelText;
    public Text playerAtkLevelUpPriceText;
    public Text playerAtkSpeedLevelUpPriceText;

    void Start()
    {
        SetShopPanel();
    }

    void SetShopPanel()
    {
        playerAtkLevelText.text = $"공격력 LV.{GameManager.instance.playerData.playerAtkLevel}";
        playerAtkLevelText.text = $"공격속도 LV.{GameManager.instance.playerData.playerAtkSpeedLevel}";
        playerAtkLevelUpPriceText.text =
        $"{string.Format("{0:#,###}", shopData.playerAtkLevelUpPrice[GameManager.instance.playerData.playerAtkLevel])}";
        playerAtkSpeedLevelUpPriceText.text =
        $"{string.Format("{0:#,###}", shopData.playerAtkSpeedLevelUpPrice[GameManager.instance.playerData.playerAtkSpeedLevel])}";
    }
}
