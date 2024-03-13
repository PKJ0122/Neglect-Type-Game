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

    void Awake()
    {
        gameManager = GameManager.instance;
        mountingSword = gameManager.swordData.swordDatas[gameManager.inventoryData.inventorys[gameManager.inventoryData.mountingSwordIndex]];

        SetPlayerAtk();
        SetPlayerGold();
        animator.SetFloat("isAtkSpeed", gameManager.playerData.playerAtkSpeedLevel+1);
    }
    public void SetPlayerAtk()
    {
        mountingSword = gameManager.swordData.swordDatas[gameManager.inventoryData.inventorys[gameManager.inventoryData.mountingSwordIndex]];
        playerAttText.text = $"{string.Format("{0:#,###}", PlayerAtkCalculate())}     ";
        playerWeaponImage.sprite = mountingSword.swordSprite;
    }

    public int PlayerAtkCalculate()
    {
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
    }
}
