using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int day;
    
    public int currentBlood;
    public int maxBlood;
    
    public int castleLevel;
    public float castleHealth;
    
    public int ghoulCount;
    public int maxGhouls;

    public int[,] towerLayout = new int[11, 11];
}
