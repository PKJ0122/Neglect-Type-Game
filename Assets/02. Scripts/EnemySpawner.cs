using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public EnemyInfo enemyInfo;
    public EnemyData enemyData;

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
        GameManager.instance.playerData.gold += enemyInfo.spawnEnemy.compensationGold;
        PlayerInfo.playerSetInfo.Invoke();
        Invoke("SpawnEnemy", 0.3f);
    }

    public void ChangeEnemy(Enemy enemy)
    {
        playerInfo.PlayerAtkModeChange();
        enemyInfo.spawnEnemy = enemy;
        enemyInfo.gameObject.SetActive(false);
        SpawnEnemy();
    }
}
