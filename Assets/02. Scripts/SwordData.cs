using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SwordData")]
[Serializable]
public class SwordData : ScriptableObject
{
    public Sword[] swordDatas;
}

[Serializable]
public class Sword
{
    public string name;
    public Sprite swordSprite;
    public int atk;
    public int enforcePercentage;
    public int enforceAmount;

    public int sellAmount;
}