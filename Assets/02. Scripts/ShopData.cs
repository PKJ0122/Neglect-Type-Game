using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ShopData")]
[Serializable]
public class ShopData : ScriptableObject
{
    public int[] playerAtkLevelUpPrice;
    public int[] playerAtkSpeedLevelUpPrice;
}
