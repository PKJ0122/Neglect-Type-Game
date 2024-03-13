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
    public Slider crystalCatch;

    float minValue = 0.7f;
    float maxValue = 9.3f;

    bool crystalCatchIng = false;

    moveMode moveMode = moveMode.goRight;

    int slotNumber = -1;
    int inventorySword = -1;
    int enforcePercentage;

    public Image nowSwordImage;
    public Image nextSwordImage;
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
    }

    void EnforceButton()
    {
        enforceButton.interactable = false;
        cancellationButton.interactable = false;
        crystalCatchPanel.SetActive(true);
        crystalCatchIng = true;
    }

    void CrystalCatchStop()
    {
        stopButton.interactable = false;
        float StopValue = crystalCatch.value;
        crystalCatchIng = false;

        int enforcePercentage = swordData.swordDatas[inventorySword].enforcePercentage;
        if (4.5f <= StopValue && StopValue <= 5.5f)
        {
            enforcePercentage++;
        }

        Invoke("EenforceCalculate", 2f);
    }
    
    void EenforceCalculate()
    {
        int enforceAmount = Random.Range(1, 101);

        if (enforceAmount <= enforcePercentage)
        {
            GameManager.instance.inventoryData.inventorys[slotNumber]++;
        }
        else
        {
            GameManager.instance.inventoryData.inventorys[slotNumber]--;
        }
    }
}
