using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    GameManager gameManager;

    public Sword mountingSword;

    public Text playerAttText;
    public Text playerGoldText;

    public SpriteRenderer playerWeaponImage;

    public Animator animator;

    public EnemyInfo enemyInfo;

    public static Action playerSetInfo;
    public static Action playerSetAtkMode;

    public GameObject[] damageSkin;
    int damagediscrimination = 0;

    public static Action<bool> atkPotion;
    bool atkPotionState;

    void Awake()
    {
        gameManager = GameManager.instance;
        mountingSword = gameManager.swordData.swordDatas[gameManager.inventoryData.inventorys[gameManager.inventoryData.mountingSwordIndex]];

        playerSetInfo += SetPlayerAtk;
        playerSetInfo += SetPlayerGold;

        playerSetAtkMode += PlayerAtkModeChange;

        atkPotion += AtkPotion;

        if (GameManager.instance.playerData.potionRemainingTime != 0)
            atkPotionState = true;

        playerSetInfo.Invoke();
    }

    public void SetPlayerAtk()
    {
        mountingSword = gameManager.swordData.swordDatas[gameManager.inventoryData.inventorys[gameManager.inventoryData.mountingSwordIndex]];
        playerAttText.text = $"{string.Format("{0:#,###}", PlayerAtkCalculate())}     ";
        playerWeaponImage.sprite = mountingSword.swordSprite;
    }

    public int PlayerAtkCalculate()
    {
        if(atkPotionState)
            return mountingSword.atk * (1 + gameManager.playerData.playerAtkLevel) * 5;

        return mountingSword.atk * (1 + gameManager.playerData.playerAtkLevel);
    }

    public void SetPlayerGold()
    {
        if(gameManager.playerData.gold == 0)
        {
            playerGoldText.text = $"0   ";
            return;
        }

        playerGoldText.text = $"{string.Format("{0:#,###}", gameManager.playerData.gold)}   ";
    }

    public void PlayerAtkModeChange()
    {
        animator.SetBool("isAtk", !animator.GetBool("isAtk"));
    }

    public void EnemyAtk()
    {
        enemyInfo.GetDamage(PlayerAtkCalculate());
        SoundManager.sfxPlay.Invoke(0);

        damageSkin[damagediscrimination].SetActive(true);
        damageSkin[damagediscrimination].GetComponent<DamageText>().SetDamageText(PlayerAtkCalculate());
        damagediscrimination++;

        if (damagediscrimination == damageSkin.Length)
            damagediscrimination = 0;
    }

    void AtkPotion(bool eat)
    {
        atkPotionState = eat;
        playerSetInfo.Invoke();
    }

    private void OnDestroy()
    {
        playerSetInfo -= SetPlayerAtk;
        playerSetInfo -= SetPlayerGold;
        playerSetAtkMode -= PlayerAtkModeChange;
        atkPotion -= AtkPotion;
    }
}
