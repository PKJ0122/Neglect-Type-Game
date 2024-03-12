using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaManager : MonoBehaviour
{
    public EnemyData enemyData;

    public GameObject encyclopediaButtonPrefab;
    public Transform encyclopediaButtonParent;

    public GameObject encyclopediaEnemyInfoPanel;
    public Image encyclopediaImage;
    public Text encyclopediaEnemyNumberText;
    public Text encyclopediaEnemyHpText;
    public Text encyclopediaEnemyCompensationGoldText;

    public Button enemyChangeButton;
    public Enemy selectEnemy;
    public EnemySpawner enemySpawner;

    void Start()
    {
        SetEncyclopedia();
        enemyChangeButton.onClick.AddListener(ChangeEnemyButton);
    }

    void SetEncyclopedia()
    {
        for (int i = 0; i < enemyData.enemyDatas.Length; i++)
        {
            GameObject encyclopediaButtonObject = Instantiate(encyclopediaButtonPrefab);
            encyclopediaButtonObject.transform.SetParent(encyclopediaButtonParent,false);
            encyclopediaButtonObject.GetComponent<EncyclopediaButton>().enemy = enemyData.enemyDatas[i];
            encyclopediaButtonObject.GetComponent<EncyclopediaButton>().enemyNumberText.text = $"{i+1} 번째 몬스터";
            encyclopediaButtonObject.GetComponent<EncyclopediaButton>().SetEncyclopediaButton(this);

            if(GameManager.instance.playerData.lastSpawnMosterId == i)
            {
                encyclopediaButtonObject.GetComponent<EncyclopediaButton>().discriminationMark.SetActive(true);
            }
        }
    }

    void ChangeEnemyButton()
    {
        enemySpawner.ChangeEnemy(selectEnemy);
        encyclopediaEnemyInfoPanel.SetActive(false);
        encyclopediaButtonParent.GetChild(GameManager.instance.playerData.lastSpawnMosterId)
            .gameObject.GetComponent<EncyclopediaButton>().discriminationMark.SetActive(false);

        GameManager.instance.playerData.lastSpawnMosterId = selectEnemy.enemyId;

        encyclopediaButtonParent.GetChild(GameManager.instance.playerData.lastSpawnMosterId)
            .gameObject.GetComponent<EncyclopediaButton>().discriminationMark.SetActive(true);
    }
}
