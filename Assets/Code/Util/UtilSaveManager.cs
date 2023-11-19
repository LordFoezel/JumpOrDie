using System.IO;
using UnityEngine;

public static class UtilSaveManager
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "levelData.json");

    [System.Serializable]
    public class LevelData
    {
        public int maxLevel;
        public int actualLevel;
        public int coins;
        public int health;
        public int isIngame;
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
        int actualGameLevel = GameManager.GetActualGameLevel();
        LevelData savedData = LoadLevelData();
        int maxGameLevel = savedData.maxLevel;
        if (actualGameLevel > maxGameLevel) maxGameLevel = actualGameLevel;
        LevelData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            health = savedData.health,
            coins = savedData.coins,
            isIngame = 0,
        };
        SaveLevelData(newData);
    }

       public static void SaveCurrentGame(int health, int coins)
    {
        int actualGameLevel = GameManager.GetActualGameLevel();
        LevelData savedData = LoadLevelData();
        int maxGameLevel = savedData.maxLevel;
        LevelData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            health = health,
            coins = coins,
            isIngame = 1,
        };
        SaveLevelData(newData);
    }
}
