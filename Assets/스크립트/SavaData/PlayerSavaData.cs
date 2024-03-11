using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/SaveData")]
public class PlayerSavaData : ScriptableObject
{
    public int playerAtkLevel;
    public int playerAtkSpeedLevel;
    public int gold;

    public int monsterSpawnId;
}
