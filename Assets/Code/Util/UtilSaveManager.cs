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
        public float positionX;
        public float positionY;
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
            positionX = savedData.positionX,
            positionY = savedData.positionY,
            isIngame = savedData.isIngame,
        };
        SaveLevelData(newData);
    }

       public static void SaveCurrentGame(int health, int coins, Vector2 position)
    {
        int actualGameLevel = GameManager.GetActualGameLevel();
        LevelData savedData = LoadLevelData();
        int maxGameLevel = savedData.maxLevel;
        float x = position.x;
        float y = position.y;
        LevelData newData = new()
        {
            maxLevel = maxGameLevel,
            actualLevel = actualGameLevel,
            health = health,
            coins = coins,
            isIngame = 1,
            positionX = x,
            positionY = y,
        };
        SaveLevelData(newData);
    }
}
