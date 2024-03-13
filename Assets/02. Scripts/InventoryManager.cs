using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public SwordData swordData;

    public GameObject[] inventorySlots;

    public GameObject swordInfoPanel;
    public Image swordPanelImage;
    public Text swordPanelEnforceText;
    public Text swordPanelNameText;
    public Text swordPanelNowAtkText;
    public Text swordPanelNextAtkText;
    public Text swordPanelEnforceAmountText;
    public Text swordPanelEnforcePercentageText;
    public Text swordPanelSellAmountText;

    public Button swordPanelEnforceButton;
    public Button swordPanelSellButton;
    public Button swordPanelMountingButton;

    int selectSlotNumber = -1;
    int selectSlotSword = -1;

    public GameObject mousePointer;
    public int selectPickUpSwordNumber = -1;
    public int pointOnSwordNumber = -1;

    public GameObject enforceManager;

    void Start()
    {
        InventorySetting();

        swordPanelEnforceButton.onClick.AddListener(SwordEnforce);
        swordPanelSellButton.onClick.AddListener(SwordSell);
        swordPanelMountingButton.onClick.AddListener(SwordMounting);
    }

    public void InventorySetting()
    {
        for (int i = 0; i < GameManager.instance.inventoryData.inventorys.Length; i++)
        {
            if (GameManager.instance.inventoryData.inventorys[i] == -1)
                continue;

            inventorySlots[i].transform.GetChild(0).gameObject.SetActive(true);
            inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite =
                GameManager.instance.swordData.swordDatas[GameManager.instance.inventoryData.inventorys[i]].swordSprite;

            if (GameManager.instance.inventoryData.mountingSwordIndex == i)
            {
                inventorySlots[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void OpenAndSetSwordInfo(int SlotNumber)
    {
        selectSlotSword = GameManager.instance.inventoryData.inventorys[SlotNumber];
        if (selectSlotSword == -1)
            return;

        selectSlotNumber = SlotNumber;

        swordInfoPanel.SetActive(true);
        swordPanelImage.sprite = swordData.swordDatas[selectSlotSword].swordSprite;
        swordPanelEnforceText.text = $"+{selectSlotSword}";
        swordPanelNameText.text = $"{swordData.swordDatas[selectSlotSword].name}";
        swordPanelNowAtkText.text = $"{swordData.swordDatas[selectSlotSword].atk}";
        swordPanelNextAtkText.text = $"{swordData.swordDatas[selectSlotSword + 1].atk}";
        swordPanelEnforceAmountText.text = $"{string.Format("{0:#,###}", swordData.swordDatas[selectSlotSword].enforceAmount)}";
        swordPanelEnforcePercentageText.text = $"°­È­È®·ü {swordData.swordDatas[selectSlotSword].enforcePercentage}%";
        swordPanelSellAmountText.text = swordData.swordDatas[selectSlotSword].sellAmount == 0 ?
            "0" : $"{string.Format("{0:#,###}", swordData.swordDatas[selectSlotSword].sellAmount)}";

        if (SlotNumber == GameManager.instance.inventoryData.mountingSwordIndex)
        {
            swordPanelSellButton.interactable = false;
            swordPanelMountingButton.interactable = false;
        }
        else
        {
            swordPanelSellButton.interactable = true;
            swordPanelMountingButton.interactable = true;
        }

        if (GameManager.instance.playerData.gold >= swordData.swordDatas[selectSlotSword].enforceAmount)
            swordPanelEnforceButton.interactable = true;
        else
            swordPanelEnforceButton.interactable = false;
    }

    public void PickUpSword(int SlotNumber)
    {
        if (pointOnSwordNumber == -1)
            return;

        if (GameManager.instance.inventoryData.inventorys[pointOnSwordNumber] == -1)
            return;

        selectPickUpSwordNumber = SlotNumber;
        inventorySlots[selectPickUpSwordNumber].transform.GetChild(0).gameObject.SetActive(false);
        inventorySlots[selectPickUpSwordNumber].transform.GetChild(1).gameObject.SetActive(false);
        mousePointer.SetActive(true);
        mousePointer.GetComponent<MousePointer>().imageRenderer.sprite =
            swordData.swordDatas[GameManager.instance.inventoryData.inventorys[SlotNumber]].swordSprite;
    }
    public void DropSword()
    {
        mousePointer.SetActive(false);

        if (selectPickUpSwordNumber == -1)
            return;

        if (pointOnSwordNumber == -1)
        {
            selectPickUpSwordNumber = -1;
            InventorySetting();
            return;
        }

        (GameManager.instance.inventoryData.inventorys[selectPickUpSwordNumber], GameManager.instance.inventoryData.inventorys[pointOnSwordNumber])
            = (GameManager.instance.inventoryData.inventorys[pointOnSwordNumber], GameManager.instance.inventoryData.inventorys[selectPickUpSwordNumber]);

        if (GameManager.instance.inventoryData.mountingSwordIndex == selectPickUpSwordNumber)
        {
            GameManager.instance.inventoryData.mountingSwordIndex = pointOnSwordNumber;
        }
        else if (GameManager.instance.inventoryData.mountingSwordIndex == pointOnSwordNumber)
        {
            GameManager.instance.inventoryData.mountingSwordIndex = selectPickUpSwordNumber;
            inventorySlots[pointOnSwordNumber].transform.GetChild(1).gameObject.SetActive(false);
        }


        selectPickUpSwordNumber = -1;
        swordInfoPanel.SetActive(false);
        InventorySetting();
    }

    public void PointOn(int SlotNumber)
    {
        pointOnSwordNumber = SlotNumber;
    }
    public void PointOff()
    {
        pointOnSwordNumber = -1;
    }

    void SwordEnforce()
    {
        enforceManager.SetActive(true);
        enforceManager.GetComponent<EnforceManager>().Enforce(selectSlotNumber);
        swordInfoPanel.SetActive(false);
    }

    void SwordSell()
    {
        GameManager.instance.playerData.gold += swordData.swordDatas
            [GameManager.instance.inventoryData.inventorys[selectSlotNumber]].sellAmount;
        GameManager.instance.inventoryData.inventorys[selectSlotNumber] = -1;
        inventorySlots[selectSlotNumber].transform.GetChild(0).gameObject.SetActive(false);
        InventorySetting();
        playerInfo.SetPlayerGold();
    }

    void SwordMounting()
    {
        inventorySlots[GameManager.instance.inventoryData.mountingSwordIndex].transform.GetChild(1).gameObject.SetActive(false);
        playerInfo.mountingSword = swordData.swordDatas[GameManager.instance.inventoryData.inventorys[selectSlotNumber]];
        GameManager.instance.inventoryData.mountingSwordIndex = selectSlotNumber;
        InventorySetting();
        playerInfo.SetPlayerAtk();
    }
}
