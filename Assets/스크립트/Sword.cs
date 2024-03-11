using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Sword")]
public class Sword : ScriptableObject
{
    public int grade;
    public bool playerHandSword;
    public bool swordOn;

    public Sprite[] swordImg;
    public string[] swordName;
    public int[] swordAtk;
    public int[] swordGradegold;
    public int[] swordSellgold;
}
