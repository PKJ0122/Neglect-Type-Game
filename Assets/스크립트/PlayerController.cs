using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameManager gM;
    InventoryManager iM;
    SavaDataManager sM;
    Animator aM;

    Sword hand;
    [SerializeField] GameObject handSowrdObj;
    [SerializeField] SpriteRenderer handSowrdImg;

    float playerAtk;
    public Text playerAtkSee;

    public Sword Hand { get => hand; set => hand = value; }

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        iM = GameObject.Find("InventoryManager").GetComponent <InventoryManager>();
        sM = GameObject.Find("SavaDataManager").GetComponent<SavaDataManager>();
        aM = GetComponent<Animator>();

        SetHandSowrd();
        SetAtk();
        SetAtkSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        aM.SetBool("isAtk",gM.SpawnMon);
    }
    void SetHandSowrd()
    {
        for (int i = 0; 0 < iM.Swords.Length; i++)
        {
            if (iM.Swords[i].playerHandSword)
            {
                hand = iM.Swords[i];
                return;
            }
        }
    }
    public void SetAtk()
    {
        playerAtk = hand.swordAtk[hand.grade];
        handSowrdImg.sprite = hand.swordImg[hand.grade];
        playerAtkSee.text = $"{string.Format("{0:#,###}",playerAtk)}     ";
    }
    void EnemyATK()
    {
        if (gM.SpawnMonster == null)
            return;
        gM.SpawnMonster.GetComponent<Enemy>().HPDown(playerAtk);
    }
    void SetAtkSpeed()
    {
        aM.SetFloat("isAtkSpeed", (float)sM.SD.playerAtkSpeedLevel);
    }

}
