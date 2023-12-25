using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class UtilSaveManager
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "SaveData.json");

    public static void SaveMaxLevel()
    {
        SaveGameData savedData = LoadSaveData();
        int actualGameLevel = GameManager.ActualGameLevel;
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        SaveGameData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            health = savedData.health,
            levelCoins = savedData.levelCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
            traps = savedData.traps,
            coins = savedData.coins,
            potions = savedData.potions,
        };
        SaveData(newData);
    }

    public static void ClearMaxLevel()
    {
        SaveGameData savedData = LoadSaveData();
        SaveGameData newData = new()
        {
            maxLevel = 1,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = savedData.levelCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
            traps = savedData.traps,
            coins = savedData.coins,
            potions = savedData.potions,
        };
        SaveData(newData);
    }

    public static void SaveIsIngame(int isIngame)
    {
        SaveGameData savedData = LoadSaveData();
        SaveGameData newData = new()
        {
            isIngame = isIngame,
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = savedData.levelCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            traps = savedData.traps,
            coins = savedData.coins,
            potions = savedData.potions,
        };
        SaveData(newData);
    }

    public static void SaveLevelData()
    {
        SaveGameData savedData = LoadSaveData();
        SaveGameData newData = new()
        {
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = savedData.levelCoins,
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
            traps = SavedTrapData(),
            coins = SavedCoinData(),
            potions = SavedPotionData(),
        };
        SaveData(newData);
    }

    public static void SaveCurrentGame(int health, int coins, Vector2 position)
    {
        SaveGameData savedData = LoadSaveData();
        float x = position.x;
        float y = position.y;
        int actualGameLevel = GameManager.ActualGameLevel;
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        SaveGameData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            health = health,
            levelCoins = coins,
            isIngame = 1,
            positionX = x,
            positionY = y,
            traps = SavedTrapData(),
            coins = SavedCoinData(),
            potions = SavedPotionData(),
        };
        SaveData(newData);
    }

    public static void ClearLevelSave()
    {
        SaveGameData savedData = LoadSaveData();
        SaveGameData newData = new()
        {
            maxLevel = savedData.maxLevel,
            actualLevel = savedData.actualLevel,
            health = savedData.health,
            levelCoins = 0,
            positionX = 0,
            positionY = 0,
            isIngame = savedData.isIngame,
            traps = new List<TrapData>(),
            coins = new List<int>(),
            potions = new List<int>(),
        };
        SaveData(newData);
    }

    #region <<< Helpers >>>

    public static void SaveData(SaveGameData SaveData)
    {
        string json = JsonUtility.ToJson(SaveData);
        File.WriteAllText(filePath, json);
    }

    public static SaveGameData LoadSaveData()
    {
        if (!File.Exists(filePath)) return new SaveGameData();
        return JsonUtility.FromJson<SaveGameData>(File.ReadAllText(filePath));
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
            int isReady = UtilBool.BoolToInt(trap.Value.IsReady);
            savedTrapData.Add(new TrapData() { id = trap.Key, isReady = isReady });
        }
        return savedTrapData;
    }

    #endregion
}
