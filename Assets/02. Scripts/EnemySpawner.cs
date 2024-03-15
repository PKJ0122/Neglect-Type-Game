using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public EnemyInfo enemyInfo;
    public EnemyData enemyData;

    public GameObject[] goldSkin;
    int golddiscrimination = 0;

    void Awake()
    {
        enemyInfo.spawnEnemy = enemyData.enemyDatas[GameManager.instance.playerData.lastSpawnMosterId];
    }

    void Start()
    {
        Invoke("SpawnEnemy", 0.3f);
    }

    public void SpawnEnemy()
    {
        enemyInfo.gameObject.SetActive(true);
        PlayerInfo.playerSetAtkMode.Invoke();
    }

    public void DieEnemy()
    {
        enemyInfo.gameObject.SetActive(false);
        int gold = enemyInfo.spawnEnemy.compensationGold;
        GameManager.instance.playerData.gold += gold;
        PlayerInfo.playerSetInfo.Invoke();
        Invoke("SpawnEnemy", 0.3f);

        goldSkin[golddiscrimination].SetActive(true);
        goldSkin[golddiscrimination].GetComponent<DamageText>().SetDamageText(gold,true);
        golddiscrimination++;

        if(golddiscrimination == goldSkin.Length)
            golddiscrimination = 0;
    }

    public void ChangeEnemy(Enemy enemy)
    {
        playerInfo.PlayerAtkModeChange();
        enemyInfo.spawnEnemy = enemy;
        enemyInfo.gameObject.SetActive(false);
        SpawnEnemy();
    }
}
