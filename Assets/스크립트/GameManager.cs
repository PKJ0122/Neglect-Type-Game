using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    SavaDataManager sM;

    bool spawnMon;
    [SerializeField] Slider enemyHpBar;

    [SerializeField] int gold;
    [SerializeField] Text goldSee;
    [SerializeField] Text playerAtkSee;

    [SerializeField] GameObject[] spawnMonsters;
    [SerializeField] int[] monsterRiwards;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject spawnMonster;
    [SerializeField] Enemy enemyInfo;



    public bool SpawnMon { get => spawnMon; set => spawnMon = value; }
    public GameObject SpawnMonster { get => spawnMonster; set => spawnMonster = value; }

    void Start()
    {
        sM = GameObject.Find("SavaDataManager").GetComponent<SavaDataManager>();
        MonsterSpawn();
        SetGold();
    }
    void MonsterSpawn()
    {
        spawnMonster = Instantiate(spawnMonsters[sM.SD.monsterSpawnId], spawnPoint.position, Quaternion.identity);
        enemyInfo = spawnMonster.GetComponent<Enemy>();

        enemyHpBar.maxValue = enemyInfo.MaxHp;
        enemyHpBar.minValue = 0;
        SetEnemyHpBar();

        spawnMon = true;
        enemyHpBar.gameObject.SetActive(spawnMon);
    }
    public void MonsterDie()
    {
        spawnMon = false;
        sM.SD.gold += monsterRiwards[sM.SD.monsterSpawnId];
        SetGold();
        enemyHpBar.gameObject.SetActive(spawnMon);
        Invoke("MonsterSpawn", 2f);
    }
    public void SetGold()
    {
        goldSee.text = $"{string.Format("{0:#,###}", sM.SD.gold)}   ";
    }
    public void SetEnemyHpBar()
    {
        enemyHpBar.value = enemyInfo.Hp;
    }
}
