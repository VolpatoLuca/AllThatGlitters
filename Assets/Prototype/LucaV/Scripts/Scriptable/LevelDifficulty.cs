using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "LevelDifficulty")]
public class LevelDifficulty : ScriptableObject
{
    public string difficulty;
    public int roomAmount;
    public int friendlyRobotsAmount;
    public int enemyRobotsAmount;
}
