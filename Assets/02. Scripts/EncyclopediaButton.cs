using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaButton : MonoBehaviour
{
    public Enemy enemy;
    public Text enemyNumberText;
    public GameObject discriminationMark;

    Button OnEnemyInfoButton;

    public void SetEncyclopediaButton(EncyclopediaManager encyclopediaManager)
    {
        OnEnemyInfoButton = gameObject.GetComponent<Button>();
        OnEnemyInfoButton.onClick.RemoveAllListeners();
        OnEnemyInfoButton.onClick.AddListener(() => OnOffEnemyInfoPanel(encyclopediaManager));
    }

    void OnOffEnemyInfoPanel(EncyclopediaManager encyclopediaManager)
    {
        encyclopediaManager.selectEnemy = enemy;
        encyclopediaManager.encyclopediaEnemyInfoPanel.SetActive(true);
        encyclopediaManager.encyclopediaImage.sprite = enemy.enemySprite;
        encyclopediaManager.encyclopediaEnemyNumberText.text = $"{(enemy.enemyId)+1}번째 몬스터";
        encyclopediaManager.encyclopediaEnemyHpText.text = $"{enemy.enemyHp}";
        encyclopediaManager.encyclopediaEnemyCompensationGoldText.text = $"{enemy.compensationGold}";
        ButtonManager.buttonDownSfx.Invoke();
    }
}
