using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class UtilSaveManager
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "levelData.json");

    [System.Serializable]
    public class TrapData
    {
        public int id;
        public int isActive;
    }


    [System.Serializable]
    public class LevelData
    {
        public int maxLevel;
        public int actualLevel;
        public int totalCoins;
        public int levelCoins;
        public int health;
        public int isIngame;
        public float positionX;
        public float positionY;
        public List<TrapData> traps;
        public List<int> coins;
    }

    public static void SaveLevelData(LevelData levelData)
    {
        string json = JsonUtility.ToJson(levelData);
        File.WriteAllText(filePath, json);
    }

    public static LevelData LoadLevelData()
    {
        if (!File.Exists(filePath)) return new LevelData();
        return JsonUtility.FromJson<LevelData>(File.ReadAllText(filePath));
    }

    public static void SaveCurrentLevel()
    {
        int actualGameLevel = GameManager.ActualGameLevel;
        LevelData savedData = LoadLevelData();
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        LevelData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            health = savedData.health,
            levelCoins = savedData.levelCoins,
            totalCoins = savedData.totalCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
            traps = savedData.traps,
        };
        SaveLevelData(newData);
    }

    public static void SaveCurrentGame(int health, int coins, Vector2 position)
    {
        int actualGameLevel = GameManager.ActualGameLevel;
        LevelData savedData = LoadLevelData();
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        float x = position.x;
        float y = position.y;
        LevelData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            health = health,
            totalCoins = savedData.totalCoins,
            levelCoins = coins,
            isIngame = 1,
            positionX = x,
            positionY = y,
            traps = SavedTrapData(),
        };
        SaveLevelData(newData);
    }

    public static void SaveTotalCoins()
    {
        LevelData savedData = LoadLevelData();
        int newTotalCoins = savedData.totalCoins + savedData.levelCoins;
        LevelData newData = new()
        {
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = 0,
            totalCoins = newTotalCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
            traps = savedData.traps,
        };
        SaveLevelData(newData);
    }

    public static List<int> SavedCoinData()
    {
        List<int> savedCoinData = new List<int>();
        return savedCoinData;
    }

    public static List<TrapData> SavedTrapData()
    {
        List<TrapData> savedTrapData = new List<TrapData>();
        foreach (KeyValuePair<int, TrapBase> trap in GameManager.Traps)
        {
            int isActive = 0;
            if (trap.Value.IsActive) isActive = 1;
            savedTrapData.Add(new TrapData() { id = trap.Key, isActive = isActive });
        }
        return savedTrapData;
    }
}
