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
        public float positionX;
        public float positionY;
        public int isIngame;
        public List<TrapData> traps;
        public List<int> coins;
        public List<int> potions;
    }

    public static void SaveMaxLevel()
    {
        LevelData savedData = LoadLevelData();
        int actualGameLevel = GameManager.ActualGameLevel;
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
            coins = savedData.coins,
            potions = savedData.potions,
        };
        SaveLevelData(newData);
    }

    public static void SaveTotalCoins(int coins)
    {
        LevelData savedData = LoadLevelData();
        int newTotalCoins = savedData.totalCoins + coins;
        LevelData newData = new()
        {
            levelCoins = 0,
            totalCoins = newTotalCoins,
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
            traps = savedData.traps,
            coins = savedData.coins,
            potions = savedData.potions,
        };
        SaveLevelData(newData);
    }

    public static void SaveIsIngame(int isIngame)
    {
        LevelData savedData = LoadLevelData();
        LevelData newData = new()
        {
            isIngame = isIngame,
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = savedData.levelCoins,
            totalCoins = savedData.totalCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            traps = savedData.traps,
            coins = savedData.coins,
            potions = savedData.potions,
        };
        SaveLevelData(newData);
    }

    public static void SaveLevelData()
    {
        LevelData savedData = LoadLevelData();
        LevelData newData = new()
        {
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = savedData.levelCoins,
            totalCoins = savedData.totalCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
            traps = SavedTrapData(),
            coins = SavedCoinData(),
            potions = SavedPotionData(),
        };
        SaveLevelData(newData);
    }

    public static void SaveCurrentGame(int health, int coins, Vector2 position)
    {
        LevelData savedData = LoadLevelData();
        float x = position.x;
        float y = position.y;
        int actualGameLevel = GameManager.ActualGameLevel;
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        LevelData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            totalCoins = savedData.totalCoins,
            health = health,
            levelCoins = coins,
            isIngame = 1,
            positionX = x,
            positionY = y,
            traps = SavedTrapData(),
            coins = SavedCoinData(),
            potions = SavedPotionData(),
        };
        SaveLevelData(newData);
    }

       public static void ClearLevelSave()
    {
        LevelData savedData = LoadLevelData();
        LevelData newData = new()
        {
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = 0,
            totalCoins = savedData.totalCoins,
            positionX = 0,
            positionY = 0,
            isIngame = savedData.isIngame,
            traps = new List<TrapData>(),
            coins = new List<int>(),
            potions = new List<int>(),
        };
        SaveLevelData(newData);
    }

    #region <<< Helpers >>>

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

    public static List<int> SavedCoinData()
    {
        List<int> savedData = new List<int>();
        foreach (KeyValuePair<int, CoinBase> item in GameManager.Coins)
        {
            savedData.Add(item.Key);
        }
        return savedData;
    }

        public static List<int> SavedPotionData()
    {
        List<int> savedData = new List<int>();
        foreach (KeyValuePair<int, PotionBase> item in GameManager.Potions)
        {
            savedData.Add(item.Key);
        }
        return savedData;
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

    #endregion
}
