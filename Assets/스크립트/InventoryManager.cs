using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    PlayerController pC;

    [SerializeField] GameObject[] slots;
    [SerializeField] Sword[] swords;
    [SerializeField] GameObject[] slotsInfo;


    [SerializeField] public GameObject mousePoint;
    public static bool pick;
    public static float pickTime;
    public static int picksoltId = -1;

    public GameObject Inven;
    public Button InvenBt;
    public Button InvenCloseBt;

    public static int clickId = -1;
    public GameObject swordInfoWindow;
    public Image siImg;
    public Text siName;
    public Text siGrade;
    public Text siNowAtk;
    public Text siNextAtk;
    public Text siGradeUp;
    public Text siGradeUpGold;
    public Button siGradeBt;
    public Text siSellGold;
    public Button siSellBt;
    public Button siHandBt;
    public Button siCloseBt;


    public GameObject[] Slots { get => slots; set => slots = value; }
    public Sword[] Swords { get => swords; set => swords = value; }

    // Start is called before the first frame update
    void Start()
    {
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
        InvenBt.onClick.AddListener(OnOffInven);
        InvenCloseBt.onClick.AddListener(OnOffInven);
        siHandBt.onClick.AddListener(SetHandSword);
        siCloseBt.onClick.AddListener(SwordInfoCloseBt);
    }

    // Update is called once per frame
    void Update()
    {
        if (pick)
        {
            mousePoint.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }
    }
    private void LateUpdate()
    {
        if (pick)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SetSlot();
                picksoltId = -1;
                mousePoint.SetActive(false);
                pick = false;
            }
        }
    }
    public void SetSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetActive(swords[i].swordOn);
            if (swords[i].swordOn)
            {
                slotsInfo[2 * i].gameObject.GetComponent<Image>().sprite = swords[i].swordImg[swords[i].grade];
                slotsInfo[2 * i + 1].gameObject.SetActive(swords[i].playerHandSword);
            }
        }
    }
    public void SetSwordInfo()
    {
        swordInfoWindow.SetActive(true);
        siImg.sprite = swords[clickId].swordImg[swords[clickId].grade];
        siName.text = swords[clickId].swordName[swords[clickId].grade];
        siGrade.text = $"+{swords[clickId].grade}";
        siNowAtk.text = $"{string.Format("{0:#,###}", swords[clickId].swordAtk[swords[clickId].grade])}";
        siNextAtk.text = $"{string.Format("{0:#,###}", swords[clickId].swordAtk[swords[clickId].grade + 1])}";
        siGradeUp.text = $"°­È­È®·ü {100 - swords[clickId].grade * 4}%";
        siGradeUpGold.text = $"{string.Format("{0:#,###}", swords[clickId].swordGradegold[swords[clickId].grade])}";
        siSellGold.text = $"{string.Format("{0:#,###}", swords[clickId].swordSellgold[swords[clickId].grade])}";
    }
    void SwordInfoCloseBt()
    {
        swordInfoWindow.SetActive(false);
        clickId = -1;
    }
    void SetHandSword()
    {
        pC.Hand = swords[clickId];
        for (int i = 0; i < swords.Length; i++)
        {
            swords[i].playerHandSword = false;
        }
        swords[clickId].playerHandSword = true;
        pC.SetAtk();
        OnOffInven();
        SwordInfoCloseBt();
        SetSlot();
    }
    void OnOffInven()
    {
        Inven.SetActive(!Inven.activeSelf);
        if(Inven.activeSelf)
            SetSlot();
        else
            swordInfoWindow.SetActive(false);
    }
}
