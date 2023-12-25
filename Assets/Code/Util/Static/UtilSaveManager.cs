using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class UtilSaveManager
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "SaveData.json");

    [System.Serializable]
    public class TrapData
    {
        public int id;
        public int isActive;
    }

    [System.Serializable]
    public class SaveData
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
        SaveData savedData = LoadSaveData();
        int actualGameLevel = GameManager.ActualGameLevel;
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        SaveData newData = new()
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
        SaveSaveData(newData);
    }

    public static void SaveTotalCoins(int coins)
    {
        SaveData savedData = LoadSaveData();
        int newTotalCoins = savedData.totalCoins + coins;
        SaveData newData = new()
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
        SaveSaveData(newData);
    }

    public static void SaveIsIngame(int isIngame)
    {
        SaveData savedData = LoadSaveData();
        SaveData newData = new()
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
        SaveSaveData(newData);
    }

    public static void SaveSaveData()
    {
        SaveData savedData = LoadSaveData();
        SaveData newData = new()
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
        SaveSaveData(newData);
    }

    public static void SaveCurrentGame(int health, int coins, Vector2 position)
    {
        SaveData savedData = LoadSaveData();
        float x = position.x;
        float y = position.y;
        int actualGameLevel = GameManager.ActualGameLevel;
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        SaveData newData = new()
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
        UtilDebug.Log(actualGameLevel);
        SaveSaveData(newData);
    }

    public static void ClearLevelSave()
    {
        SaveData savedData = LoadSaveData();
        SaveData newData = new()
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
        SaveSaveData(newData);
    }

    #region <<< Helpers >>>

    public static void SaveSaveData(SaveData SaveData)
    {
        string json = JsonUtility.ToJson(SaveData);
        File.WriteAllText(filePath, json);
    }

    public static SaveData LoadSaveData()
    {
        if (!File.Exists(filePath)) return new SaveData();
        return JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));
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
