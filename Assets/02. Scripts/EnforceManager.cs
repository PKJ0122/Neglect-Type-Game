using UnityEngine;
using UnityEngine.UI;

enum moveMode
{
    goLeft,
    goRight
}

public class EnforceManager : MonoBehaviour
{
    public SwordData swordData;

    public GameObject enforcePanel;
    public GameObject crystalCatchPanel;
    public Image crystalCatchPanelSwordImage;
    public Slider crystalCatch;
    public GameObject enforceResultPanel;
    public GameObject enforceSuccessText;
    public GameObject enforceFailText;
    public Image enforceResultPanelSwordImage;
    public Text enforceResultPanelSwordAmountText;

    float minValue = 0.7f;
    float maxValue = 9.3f;

    bool crystalCatchIng = false;

    moveMode moveMode = moveMode.goRight;

    int slotNumber = -1;
    int inventorySword = -1;
    int enforcePercentage = -1;

    public Image nowSwordImage;
    public Image nextSwordImage;
    public Text nowSwordAmountText;
    public Text nextSwordAmountText;
    public Button enforceButton;
    public Button cancellationButton;
    public Button stopButton;

    void Awake()
    {
        enforceButton.onClick.AddListener(EnforceButton);
        stopButton.onClick.AddListener(CrystalCatchStop);
    }

    void OnEnable()
    {
        crystalCatch.value = 0.7f;
        moveMode = moveMode.goRight;
        enforceButton.interactable = true;
        cancellationButton.interactable = true;
        stopButton.interactable = true;
    }

    void Update()
    {
        if (!crystalCatchIng)
            return;

        if(moveMode == moveMode.goRight)
        {
            crystalCatch.value += Time.deltaTime*15f;
            if(crystalCatch.value >= maxValue)
            {
                crystalCatch.value = maxValue;
                moveMode = moveMode.goLeft;
            }
        }
        else
        {
            crystalCatch.value -= Time.deltaTime*15f;
            if (crystalCatch.value <= minValue)
            {
                crystalCatch.value = minValue;
                moveMode = moveMode.goRight;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            CrystalCatchStop();
    }

    public void Enforce(int SlotNumber)
    {
        slotNumber = SlotNumber;
        enforcePanel.SetActive(true);
        inventorySword = GameManager.instance.inventoryData.inventorys[slotNumber];
        nowSwordImage.sprite = swordData.swordDatas[inventorySword].swordSprite;
        nextSwordImage.sprite = swordData.swordDatas[inventorySword+1].swordSprite;
        nowSwordAmountText.text = $"+{inventorySword}";
        nextSwordAmountText.text = $"+{inventorySword+1}";
    }

    void EnforceButton()
    {
        enforceButton.interactable = false;
        cancellationButton.interactable = false;
        crystalCatchPanel.SetActive(true);
        crystalCatchIng = true;
        crystalCatchPanelSwordImage.sprite = swordData.swordDatas[GameManager.instance.inventoryData.inventorys[slotNumber]].swordSprite;
    }

    void CrystalCatchStop()
    {
        stopButton.interactable = false;
        float StopValue = crystalCatch.value;
        crystalCatchIng = false;

        enforcePercentage = swordData.swordDatas[inventorySword].enforcePercentage;
        if (4.5f <= StopValue && StopValue <= 5.5f)
        {
            enforcePercentage++;
        }

        Invoke("EenforceCalculate", 1f);
    }
    
    void EenforceCalculate()
    {
        GameManager.instance.playerData.gold -= swordData.swordDatas[inventorySword].enforceAmount;

        int enforceAmount = Random.Range(1, 101);

        enforceResultPanel.SetActive(true);

        if (enforceAmount <= enforcePercentage)
        {
            GameManager.instance.inventoryData.inventorys[slotNumber]++;
            enforceSuccessText.SetActive(true);
        }
        else
        {
            GameManager.instance.inventoryData.inventorys[slotNumber]--;
            enforceFailText.SetActive(true);
        }

        enforceResultPanelSwordImage.sprite = swordData.swordDatas[GameManager.instance.inventoryData.inventorys[slotNumber]].swordSprite;
        enforceResultPanelSwordAmountText.text = $"+{GameManager.instance.inventoryData.inventorys[slotNumber]}";
        Enforce(slotNumber);
        InventoryManager.inventorySet.Invoke();
        PlayerInfo.playerSetInfo.Invoke();
    }
}
