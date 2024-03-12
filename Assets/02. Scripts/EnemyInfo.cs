using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public Enemy spawnEnemy;
    public EnemySpawner enemySpawner;

    public Image enemyImage;
    public Slider enemyHpSlider;

    int enemyHp;

    void OnEnable()
    {
        enemyImage.sprite = spawnEnemy.enemySprite;
        enemyHp = spawnEnemy.enemyHp;
        enemyHpSlider.minValue = 0;
        enemyHpSlider.maxValue = enemyHp;
        enemyHpSlider.value = enemyHp;
    }

    public void GetDamage(int atk)
    {
        enemyHp -= atk;
        enemyHpSlider.value = enemyHp;

        if(enemyHp <= 0)
        {
            enemySpawner.DieEnemy();
        }
    }
}
