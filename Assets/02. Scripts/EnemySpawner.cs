using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        playerInfo.PlayerAtkModeChange();
    }
    public void DieEnemy()
    {
        enemyInfo.gameObject.SetActive(false);
        playerInfo.PlayerAtkModeChange();
        GameManager.instance.playerData.gold += enemyInfo.spawnEnemy.compensationGold;
        playerInfo.SetPlayerGold();
        Invoke("SpawnEnemy", 0.3f);
    }
}
