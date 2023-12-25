using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{
    public int maxLevel;
    public int actualLevel;
    public int levelCoins;
    public int health;
    public float positionX;
    public float positionY;
    public int isIngame;
    public List<TrapData> traps;
    public List<int> coins;
    public List<int> potions;
}
