using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
[Serializable]
public class EnemyData : ScriptableObject
{
    public Enemy[] enemyDatas;
}
